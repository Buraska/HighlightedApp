import httpCLient from "@/http-client";
import { useAppUserStore } from "@/Stores/AppUserStore";
import type { AxiosError, Method } from "axios";
import { AppUserService } from "./AppUserService";
import type { IServiceResult } from "@/Domain/ConnectionModels/IServiceResult";

export class BaseService<TEntity> {
  identityStore = useAppUserStore();

  constructor(private path: string) {}

  async wrapper(type: string, data?: any) {
    var res = this.makeRequest(type, data);
    return res;
  }

  async makeRequest(type: string, data: any): Promise<any> {
    try {
      var config = {
        headers: {
          Authorization: "bearer " + this.identityStore.$state.jwt?.token,
        },
      };

      var response;
      switch (type) {
        case "getAll":
          {
            response = await httpCLient.get(`/${this.path}`, config);
          }
          return response.data as TEntity[];
        case "get": {
          response = await httpCLient.get(`/${this.path}/${data}`, config);
          return response.data as TEntity;
        }
        case "edit": {
          Object.assign(config["headers"], {
            "Content-type": "application/json",
          });
          response = await httpCLient.put(
            `/${this.path}/${data.id}`,
            data,
            config
          );
          return response.data as TEntity;
        }
        case "add": {
          response = await httpCLient.post(`/${this.path}`, data, config);
          break;
        }

        case "delete": {
          let response = await httpCLient.delete(`/${this.path}/${data}`, config)
          break;
        }
      }
    } catch (e) {
      console.log("HERRRRRRE");

      let response = (e as AxiosError).response!;
      console.log(e);
      if (response.status == 401 && this.identityStore.jwt) {
        let appUserService = new AppUserService();
        await appUserService.refreshIdentity();
        if (this.identityStore.$state.jwt) {
          return this.makeRequest(type, data);
        }
      }
    }

    return null;
  }

  async getAll(): Promise<TEntity[]> {
    return await this.wrapper("getAll");
  }

  async get(id: string): Promise<TEntity> {
    return await this.wrapper("get", id);
  }

  async edit(item: TEntity): Promise<TEntity> {
    console.log("edit");
    return await this.wrapper("edit", item);
  }

  async delete(id: string): Promise<TEntity> {
    return await this.wrapper("delete", id);
  }

  async add(entity: TEntity): Promise<IServiceResult<void>> {
    return await this.wrapper("add", entity);
  }
}
