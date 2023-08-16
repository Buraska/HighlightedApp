import type { IJWTResponse } from "@/Domain/ConnectionModels/IJWTResponse";
import { defineStore } from "pinia";
import httpCLient from "@/http-client";

export const useAppUserStore = defineStore("appUser", {
  state: () => ({
    jwt: JSON.parse(localStorage.getItem("auth")!) as IJWTResponse | null
  }),
  getters: {
  },
  actions: {
  },
});
