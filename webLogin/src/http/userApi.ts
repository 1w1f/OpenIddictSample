import AuthServerClient from "./AuthServerClient";

export const loginApi = async (config: loginVo | undefined = undefined) => {
  return await AuthServerClient({
    method: "post",
    url: "/User/login",
    data: config,
  });
};
