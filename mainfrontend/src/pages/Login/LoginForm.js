import {useState} from "react";
import axios from "axios";
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
            setErrorMessage(error.response.data)
        }
    }

    function handleSignUp()
    {
        if (signUpCallback)
            signUpCallback()
    }


    return (
        <>
            <form>
                Login Page
                <label>
                    Login:
                    <input type={"text"}
                           name={"login"}
                           onChange={e => setLogin(e.target.value)}/>
                </label>
                <label>
                    Hasło:
                    <input type={"password"}
                           name={"password"}
                           onChange={e => setPassword(e.target.value)}/>
                </label>
                <label>
                    Zapomniałeś hasła?
                </label>
                <label>
                    <input type={"checkbox"}
                           name={"rememberMe"}/>
                    Zapamietaj mnie
                </label>
                <input type={"button"}
                       value={"Zaloguj się"}
                       onClick={handleSubmit}/>
                {isLoginFailed ? errorMessage : null}
                <input type={"button"}
                       value={"Zarejestruj się"}
                       onClick={handleSignUp}/>
                <input type={"button"}
                       value={"Zaloguj się z Google"}/>
                <input type={"button"}
                       value={"Zaloguj się z Fb"}/>
            </form>
        </>
    )
}