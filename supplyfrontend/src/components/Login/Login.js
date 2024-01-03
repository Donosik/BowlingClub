import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {setAuth, supplyBackendApi} from "../../util/Requests";

export default function Login()
{
    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isLoginFailed, setIsLoginFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("");
    const navigate=useNavigate()

    async function loginUser()
    {
        setIsLoginFailed(false)
        setErrorMessage("")

        // Validation logic
        if (login.length < 3 || !/^[a-zA-Z0-9]+$/.test(login))
        {
            setIsLoginFailed(true);
            setErrorMessage(
                "Nieprawidłowy login."
            );
            return;
        }

        if (!/(?=.*\d)(?=.*[@\-!]).{8,}/.test(password))
        {
            setIsLoginFailed(true);
            setErrorMessage(
                "Nieprawidłowe hasło."
            );
            return;
        }

        try
        {
            const request = {
                Login: login,
                Password: password
            }
            const response = await supplyBackendApi.post('User/Login', request)
            setAuth(response.data)
            navigate('/')
        } catch (e)
        {
            setIsLoginFailed(true);
            setErrorMessage("ISTNIEJE JUŻ KONTO Z TYM LOGINEM");
            console.log(e)
        }
    }

    return (
        <div className="auth-page">
            <div className="container page d-flex justify-content-center align-items-center">
                <div className="row">
                    <div className="form-box">
                        <h1 className="text-login">LOGOWANIE</h1>
                        <label>
                            Login: <input type={"text"}
                                          onChange={(e) => setLogin(e.target.value)}/>
                        </label>
                        <br/>
                        <label>
                            Hasło: <input type={"password"}
                                          onChange={(e) => setPassword(e.target.value)}/>
                        </label>
                        <br/>
                        <div className="error-message">
                            {isLoginFailed ? errorMessage : null}</div>
                        <button onClick={loginUser}>ZALOGUJ SIĘ</button>
                    </div>
                </div>
            </div>
        </div>
    )
}