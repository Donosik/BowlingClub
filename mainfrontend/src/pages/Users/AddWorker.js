import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function AddWorker() {
    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [dateOfBirth, setDateOfBirth] = useState('')
    const [isRegisterFailed, setIsRegisterFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    async function handleSubmit(e) {
        e.preventDefault()

        setIsRegisterFailed(false)
        setErrorMessage('')

        try {
            const requestData = {
                "login": login,
                "password": password,
                "firstName": firstName,
                "lastName": lastName,
                "email": email,
                "dateOfBirth": dateOfBirth
            }
            const response = await mainBackendApi.post('User/RegisterWorker', requestData)
            console.log(response)
        } catch (error) {
            setIsRegisterFailed(true)
            setErrorMessage("Błąd")
            console.log(error)
        }

    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">DODAWANIE PRACOWNIKA</h1>
                            <label>
                                Login:<br/>
                                <input type={"text"}
                                       name={"login"}
                                       onChange={e => setLogin(e.target.value)}/>
                            </label>
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
                            </label>
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
                            </label>
                            <label>
                                Data urodzenia:<br/>
                                <input type={"date"}
                                       name={"dateOfBirth"}
                                       onChange={e => setDateOfBirth(e.target.value)}/>
                            </label><br/>

                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button" onClick={handleSubmit}>ZAREJESTRUJ PRACOWNIKA</button>
                            </div>
                            {isRegisterFailed ? errorMessage : null}

                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}