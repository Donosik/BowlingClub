import LoginForm from "./LoginForm";
import {useState} from "react";
import RegisterForm from "./RegisterForm";

export default function LoginPage()
{
    const [isLogin, setIsLogin] = useState(true)

    function signUpCallback()
    {
        setIsLogin(!isLogin)
        console.log(isLogin)
    }

    return (
        <>
            {isLogin ? <LoginForm signUpCallback={signUpCallback}/> : <RegisterForm/>}
        </>
    )
}