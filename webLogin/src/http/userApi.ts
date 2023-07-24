import { AxiosRequestConfig } from "axios";
import AuthServerClient from "./AuthServerClient";

export const login = async (config: AxiosRequestConfig<any> | undefined) => {
  return await AuthServerClient.get("/login", config);
};
