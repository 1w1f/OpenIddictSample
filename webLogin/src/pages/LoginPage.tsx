import { FC, memo, useCallback, useEffect, useState } from "react";
import { Input, Button, Label, InputProps } from "@fluentui/react-components"
import { NavigateFunction, useNavigate } from "react-router-dom";

const LoginPage: FC = () => {
    const [username, setUserName] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [loginEnable, setLoginEnable] = useState<boolean>(false)


    const naviFunc: NavigateFunction = useNavigate();
    const loginCommand = useCallback(() => {
        console.log(username, password);
    }, [username, password])

    const registry = () => { naviFunc("/registry") }

    useEffect(() => {
        if (username !== "" && password !== "") {
            setLoginEnable(true)
        } else { setLoginEnable(false) }
    }, [username, password])


    const onPassWordChange: InputProps["onChange"] = (_, data) => {
        if (data.value.length <= 20) {
            setPassword(data.value);
        }
    };


    const onUserNameChange: InputProps["onChange"] = (_, data) => {
        if (data.value.length <= 20) {
            setUserName(data.value)
        }
    }

    return (
        <div className="bg-[url('../../public/GridBackground.png')]">

            <div className="relative   w-screen h-screen">
                <div className="absolute -translate-y-[50%] top-1/2 left-1/2 -translate-x-[50%]  w-[500px] h-[230px] ">
                    <div className="bg-[#bdbdbd] rounded-lg grid grid-rows-[30%_70%]  h-full">
                        <div className="flex h-full justify-center items-center align-middle text-[30px]">统一认证平台</div>
                        <div>
                            <div className="grid gap-y-3">
                                <div className="grid grid-flow-col grid-cols-[35%_40%]">
                                    <div className="flex justify-end items-center">
                                        <Label style={{ paddingInlineEnd: '8px' }} >用户名</Label>
                                    </div>
                                    <Input value={username} onChange={onUserNameChange}></Input>
                                </div>
                                <div className="grid grid-flow-col grid-cols-[35%_40%]">
                                    <div className="flex justify-end items-center">
                                        <Label style={{ paddingInlineEnd: '8px' }}>密 码</Label>
                                    </div>
                                    <Input type="password" onChange={onPassWordChange} value={password}></Input>
                                </div>
                                <div className="grid grid-cols-4 gap-x-2 justify-items-center">
                                    <Button className="col-start-2" disabled={!loginEnable} onClick={loginCommand}>登录</Button>
                                    <Button className="col-start-3" onClick={registry }>注册</Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        </div >
    )
}

export default memo(LoginPage)
