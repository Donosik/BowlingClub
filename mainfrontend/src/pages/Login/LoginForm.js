import React, {useState} from "react";
import './login.css';
import google from './google_icon.png'
import {mainBackendApi, setAuth} from "../../util/Requests";
import {Link, useNavigate} from "react-router-dom";

export default function LoginForm(props)
{
    const {signUpCallback} = props

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isLoginFailed, setIsLoginFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    const navigate=useNavigate()

    async function handleSubmit(e)
    {
        e.preventDefault()

        setIsLoginFailed(false)
        setErrorMessage('')

        try
        {
            const requestData = {
                "login": login,
                "password": password
            }
            const response = await mainBackendApi.post('User/Login',requestData)
            setAuth(response.data)
            navigate('/management')
        } catch (error)
        {
            setIsLoginFailed(true)
            setErrorMessage("Błąd")
            console.log(error)
        }
    }

    function handleSignUp()
    {
        if (signUpCallback)
            signUpCallback()
    }


    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center ">
                    <div className="row">
                        <div className="login-box">
                            <h1 className="text-login">LOGOWANIE</h1>
                            <form>
                                <label>
                                    Login:
                                    <input type="text" name="login" onChange={e => setLogin(e.target.value)}/>
                                </label><br/>
                                <label>
                                    Hasło:
                                    <input type="password" name="password" onChange={e => setPassword(e.target.value)}/>
                                </label><br/>

                                    <input type="checkbox" name="rememberMe"/>Zapamiętaj mnie


                                <div className="forgot-pass d-flex justify-content-center align-items-center">
                                    <label>Zapomniałeś hasła?</label>
                                </div>
                                {isLoginFailed ? <div className="error-message">{errorMessage}</div> : null}
                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="submit" onClick={handleSubmit}>ZALOGUJ SIĘ</button>
                                </div>
                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="button" onClick={handleSignUp}>REJESTRACJA</button>


                                    <br/>

                                    <div className="logo-google"> <img className="google-img" src={google} alt="Google"/></div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}