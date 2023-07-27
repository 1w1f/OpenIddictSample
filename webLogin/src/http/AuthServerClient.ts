import axios, { CreateAxiosDefaults, AxiosInstance } from "axios";

const AuthServerClient: AxiosInstance = initAuthServerRequestClient({
  baseURL: "https://localhost:7228",
});

function initAuthServerRequestClient(
  config: CreateAxiosDefaults<any> | undefined
): AxiosInstance {
  const instance = axios.create(config);
  instance.interceptors.request.use(
    function (config) {
      //配置http请求头(使用cookies) https://developer.mozilla.org/zh-CN/docs/Web/API/XMLHttpRequest/withCredentials
      config.withCredentials = true;
      return config;
    },
    function (error) {
      return Promise.reject(error);
    }
  );
  return instance;
}

export default AuthServerClient;
