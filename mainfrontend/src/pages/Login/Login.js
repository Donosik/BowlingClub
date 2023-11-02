import LoginForm from "./LoginForm";
import {useState} from "react";
import RegisterForm from "./RegisterForm";

export default function Login()
{
    const [isLogin, setIsLogin] = useState(true)

    function changeForm()
    {
        setIsLogin(!isLogin)
    }

    return (
        <>
            {isLogin ? <LoginForm signUpCallback={changeForm}/> : <RegisterForm loginCallback={changeForm}/>}
        </>
    )
}