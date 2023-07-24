import axios, { CreateAxiosDefaults, AxiosInstance } from "axios";

const AuthServerClient: AxiosInstance = initAuthServerRequestClient({
  baseURL: "",
});

function initAuthServerRequestClient(
  config: CreateAxiosDefaults<any> | undefined
): AxiosInstance {
  return axios.create(config);
}

export default AuthServerClient;
