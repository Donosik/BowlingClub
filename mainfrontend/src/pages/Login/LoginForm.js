import {useState} from "react";
import axios from "axios";
import './login.css';
import {fetchAdress} from "../../util/Requests";
import {setJWT} from "../../util/Requests";

export default function LoginForm(props) {
    const {signUpCallback} = props

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isLoginFailed, setIsLoginFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    async function handleSubmit(e) {
        e.preventDefault()

        setIsLoginFailed(false)
        setErrorMessage('')

        try {
            const requestData = {
                "login": login,
                "password": password
            }
            const response = await axios.post(fetchAdress() + 'User/Login', requestData)
            setJWT(response.data)
        } catch (error) {
            setIsLoginFailed(true)
            //Jeśli fetche idą na zły endpoint to to powoduje error
            setErrorMessage(error.response.data)
        }
    }

    function handleSignUp() {
        if (signUpCallback)
            signUpCallback()
    }


    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center min-vh-100">
                    <div className="row">
                        <div className="login-box">
                            <h1 className="text-login">Logowanie</h1>
                            <form>
                                <label>
                                    Login:
                                    <input type="text" name="login" onChange={e => setLogin(e.target.value)}/>
                                </label>
                                <label>
                                    Hasło:
                                    <input type="password" name="password" onChange={e => setPassword(e.target.value)}/>
                                </label>
                                <label>
                                    <input type="checkbox" name="rememberMe"/>
                                    Zapamiętaj mnie
                                </label>
                                <div className="forgot-pass d-flex justify-content-center align-items-center">
                                    <label>Zapomniałeś hasła?</label>
                                </div>
                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="button" onClick={handleSubmit}>ZALOGUJ SIĘ</button>
                                </div>
                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="button" onClick={handleSignUp}>REJESTRACJA</button>
                                    <div className="forgot-pass d-flex justify-content-center align-items-center">

                                    </div>

                                    <br/>
                                    {isLoginFailed ? <div className="error-message">{errorMessage}</div> : null}

                                    <img src="google_icon.png" alt="Google"/>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}