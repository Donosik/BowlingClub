import {useState} from "react";
import axios from "axios";
import './login.css';
import {fetchAdress} from "../../util/Requests";
import {setJWT} from "../../util/Requests";

export default function LoginForm(props)
{
    const {signUpCallback} = props

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isLoginFailed, setIsLoginFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

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
            const response = await axios.post(fetchAdress() + 'User/Login', requestData)

            setJWT(response.data)
        } catch (error)
        {
            setIsLoginFailed(true)
            //Jeśli fetche idą na zły endpoint to to powoduje error
            //setErrorMessage(error)
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
            <div>
                <form className="form-container">
                    <div>Login Page</div>
                    <label>
                        Login:
                        <input type="text"
                               name="login"
                               onChange={e => setLogin(e.target.value)}/>
                    </label>
                    <label>
                        Hasło:
                        <input type="password"
                               name="password"
                               onChange={e => setPassword(e.target.value)}/>
                    </label>
                    <label>Zapomniałeś hasła?</label>
                    <label>
                        <input type="checkbox"
                               name="rememberMe"/>
                        Zapamiętaj mnie
                    </label>
                    <button type="button"
                            onClick={handleSubmit}>Zaloguj się
                    </button>
                    <br/>
                    {isLoginFailed ? <div className="error-message">{errorMessage}</div> : null}
                    <img src="google_icon.png"
                         alt="Google"
                         onClick={handleSignUp}/>

                    <button type="button">Google</button>

                </form>
            </div>
        </>
    )
}