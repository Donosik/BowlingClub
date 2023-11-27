import React, {useState} from "react";
import { mainBackendApi} from "../../util/Requests";
import "./login.css"

export default function RegisterForm(props)
{
    const {loginCallback} = props

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [dateOfBirth, setDateOfBirth] = useState('')
    const [isRegisterFailed, setIsRegisterFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    async function handleSubmit(e)
    {
        e.preventDefault()

        setIsRegisterFailed(false)
        setErrorMessage('')
        try{
            const reguestData={
                "login":login,
                "password":password,
                "firstName":firstName,
                "lastName":lastName,
                "email":email,
                "dateOfBirth":dateOfBirth
            }
            const response=await mainBackendApi.post('User/RegisterClient',reguestData)
            console.log(response)

        }
        catch (error)
        {
            setIsRegisterFailed(true)
            setErrorMessage("Błąd")
            console.log(error)
        }
    }

    function handleLogin()
    {
        if (loginCallback)
            loginCallback()
    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center min-vh-100">
                    <div className="row">
                        <div className="login-box">
                            <h1 className="text-login">REJESTRACJA</h1>
            <form>
                <label>
                    Login:<br/>
                    <input type={"text"}
                           name={"login"}
                           onChange={e => setLogin(e.target.value)}/>
                </label> <br/>
                <label>
                    Hasło:<br/>
                    <input type={"password"}
                           name={"password"}
                           onChange={e => setPassword(e.target.value)}/>
                </label><br/>
                <label>
                    Imię:<br/>
                    <input type={"text"}
                           name={"firstName"}
                           onChange={e => setFirstName(e.target.value)}/>
                </label><br/>
                <label>
                    Nazwisko:<br/>
                    <input type={"text"}
                           name={"lastName"}
                           onChange={e => setLastName(e.target.value)}/>
                </label><br/>
                <label>
                    Email:<br/>
                    <input type={"email"}
                           name={"email"}
                           onChange={e => setEmail(e.target.value)}/>
                </label><br/>
                <label>
                    Data urodzenia:<br/>
                    <input type={"date"}
                           name={"dateOfBirth"}
                           onChange={e => setDateOfBirth(e.target.value)}/>
                </label><br/>

                <div className="d-flex justify-content-center align-items-center">
                    <button type="button" onClick={handleSubmit}>ZAREJESTRUJ SIĘ</button>
                </div>

                <div className="d-flex justify-content-center align-items-center">
                    <button type="button" onClick={handleLogin}>REJESTRACJA Z GOOGLE</button>
                </div>

                <div className="d-flex justify-content-center align-items-center">
                    <button type="button" onClick={handleLogin}>ZALOGUJ SIĘ</button>
                </div>



                {isRegisterFailed ? errorMessage : null}

            </form>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}