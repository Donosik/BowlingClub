import LoginForm from "./LoginForm";
import {useEffect, useState} from "react";
import RegisterForm from "./RegisterForm";
import {GoogleLogin} from "@react-oauth/google";
import {jwtDecode} from "jwt-decode";

export default function Login()
{
    const [isLogin, setIsLogin] = useState(true)

    function changeForm()
    {
        setIsLogin(!isLogin)
    }

    function successResponse(response)
    {
        const userObject = jwtDecode(response.credential)
        console.log(userObject)
    }

    function errorResponse(response)
    {
        console.log(response)
    }

    return (
        <>
            {isLogin ? <LoginForm signUpCallback={changeForm}/> : <RegisterForm loginCallback={changeForm}/>}
            <GoogleLogin onSuccess={successResponse}
                         onFailure={errorResponse}/>
        </>
    )
}