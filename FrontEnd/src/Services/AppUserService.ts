import type { IJWTResponse } from "@/Domain/ConnectionModels/IJWTResponse";
import type { IServiceResult } from "@/Domain/ConnectionModels/IServiceResult";
import { useAppUserStore } from "@/Stores/AppUserStore";
import httpCLient from "@/http-client";
import type { AxiosError, AxiosResponse } from "axios";
import { data } from "jquery";
import router from "@/router";

export class AppUserService {
  appUserStore = useAppUserStore();

  async login(email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
    try {
      let loginInfo = {
        email,
        password
      }
      let response = await httpCLient.post("/identity/account/login", loginInfo);
      this.appUserStore.$state.jwt = response.data!;
      localStorage.setItem("auth", JSON.stringify(response.data))
      return {
        status: response.status,
        data: response.data as IJWTResponse
      };

    } catch (e) {
      let er = e as AxiosError;
      let response = {
        status: (e as AxiosError).response!.status,
        // @ts-ignore
        errorMsg: (e as AxiosError).response!.data.error
      }
      console.log((e as AxiosError).response);

      return response;
    }
  }

  async register(email: string, password: string, fName: string, lName: string): Promise<IServiceResult<IJWTResponse>>{
    try {
      let info = {email: email, password: password, firstname: fName, lastname:lName}
      let response = await httpCLient.post("/identity/account/register", info);
      this.appUserStore.$state.jwt = response.data!;
      localStorage.setItem("auth", JSON.stringify(response.data))
      return {
        status: response.status,
        data: response.data as IJWTResponse
      };
    }
    catch (e){
      let er = e as AxiosError;
      let response = {
        status: (e as AxiosError).response!.status,
        // @ts-ignore
        errorMsg: (e as AxiosError).response!.data.errors
      }
      return response;
    }
  }

  logout() {
    this.appUserStore.jwt = null;
    localStorage.removeItem('auth');
    router.push('/identity/login');
  }

  async refreshIdentity(): Promise<IServiceResult<IJWTResponse>> {
    try {
      let response = await httpCLient.post("/identity/account/refreshtoken",
        {
          jwt: this.appUserStore.$state.jwt?.token,
          refreshToken: this.appUserStore.$state.jwt?.refreshToken
        });

      localStorage.setItem("auth", JSON.stringify(response.data));
      this.appUserStore.$state.jwt = response.data;

      return {
        status: response.status,
        data: response.data as IJWTResponse
      };

    } catch (e) {

      let response = {
        status: (e as AxiosError).response!.status,
      }
      this.logout()
      console.log(response);
      return response;
    }
  }
}