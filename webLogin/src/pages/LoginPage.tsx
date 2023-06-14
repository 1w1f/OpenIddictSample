import { FC, memo } from "react";

const LoginPage: FC = () => {
    return (
        <div className="relative bg-red-500 w-screen h-screen">
            <div className="absolute -translate-y-[50%] top-1/2 left-1/2 -translate-x-[50%]  bg-black w-[500px] h-[200px]">
                <div className="flex-row-[2]">
                    <div>统一认证平台</div>
                    <div>
                        <div>用户名</div>
                        <div>密码</div>
                        <div>登录</div>
                        <div>注册</div>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default memo(LoginPage)
