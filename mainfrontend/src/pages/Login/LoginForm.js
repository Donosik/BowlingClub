import {useState} from "react";
import axios from "axios";
import {fetchAdress} from "../../util/Requests";
import {setJWT} from "../../util/Requests";

export default function LoginForm()
{
    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [isLoginFailed, setIsLoginFailed] = useState(false)

    async function handleSubmit(e)
    {
        e.preventDefault()

        setIsLoginFailed(false)

        //console.log(login)
        //console.log(password)

        try
        {
            const requestData={
                "login":login,
                "password":password
            }
            const response=await axios.post(fetchAdress()+'User/Login',requestData)
            setJWT(response.data)
        } catch (error)
        {
            setIsLoginFailed(true)
            console.log(error)
        }
    }


    return (
        <>
            Login Page
            <form>
                <label>
                    Login:
                    <input type={"text"} name={"login"} onChange={e => setLogin(e.target.value)}/>
                </label>
                <label>
                    Password:
                    <input type={"password"} name={"password"} onChange={e => setPassword(e.target.value)}/>
                </label>
                <input type={"button"} value={"Zaloguj się"} onClick={handleSubmit}/>
            </form>
            {isLoginFailed ? "LoginFailed" : null}
        </>
    )
}