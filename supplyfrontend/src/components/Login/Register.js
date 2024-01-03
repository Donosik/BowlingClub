import {useState} from "react";
import {supplyBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function Register()
{
    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isRegisterFailed, setIsRegisterFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("");
    const navigate=useNavigate()

    async function registerUser()
    {
        setIsRegisterFailed(false)
        setErrorMessage("")

        // Validation logic
        if (login.length < 3 || !/^[a-zA-Z0-9]+$/.test(login))
        {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Nieprawidłowy login."
            );
            return;
        }

        if (!/(?=.*\d)(?=.*[@\-!]).{8,}/.test(password))
        {
            setIsRegisterFailed(true);
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
            const response = await supplyBackendApi.post('User/Register', request)
            navigate('/Login')
        } catch (e)
        {
            setIsRegisterFailed(true);
            setErrorMessage("ISTNIEJE JUŻ KONTO Z TYM LOGINEM");
            console.log(e)
        }
    }

    return (
        <div className="auth-page">
            <div className="container page d-flex justify-content-center align-items-center">
                <div className="row">
                    <div className="form-box">
                        <h1 className="text-login">REJESTRACJA</h1>
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
                            {isRegisterFailed ? errorMessage : null}</div>
                        <button onClick={registerUser}>ZAREJESTRUJ SIĘ</button>
                    </div>
                </div>
            </div>
        </div>
    )
}