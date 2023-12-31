import {useNavigate, useParams} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function EditUser()
{
    const {id} = useParams()

    const [login, setLogin] = useState('')
    const [password, setPassword] = useState('')
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [dateOfBirth, setDateOfBirth] = useState('')
    const [user, setUser] = useState({
        login: '',
        password: '',
        person: {
            firstName: '',
            lastName: '',
            email: '',
            dateOfBirth: '',
    }})
    const [isRegisterFailed, setIsRegisterFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')
    const navigate=useNavigate()

    useEffect(() =>
    {
        getUser()
    }, []);

    async function handleSubmit(e)
    {
        e.preventDefault()

        setIsRegisterFailed(false)
        setErrorMessage('')

        try
        {
            const requestData = {
                "login": login,
                "password": password,
                "firstName": firstName,
                "lastName": lastName,
                "email": email,
                "dateOfBirth": dateOfBirth
            }
            const response = await mainBackendApi.put('User/ChangeUser/' + id, requestData)
            navigate('/management/uzytkownicy')
        } catch (error)
        {
            setIsRegisterFailed(true)
            setErrorMessage("Błąd")
            console.log(error)
        }

    }

    async function getUser()
    {
        try
        {
            const response = await mainBackendApi.get('User/GetUser/' + id)
            setUser(response.data)
            console.log(user)
            setLogin(response.data.login)
            setPassword(response.data.password)
            setFirstName(response.data.person.firstName)
            setLastName(response.data.person.lastName)
            setEmail(response.data.person.email)
            setDateOfBirth(response.data.person.dateOfBirth)
        } catch (error)
        {
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
                            <h1 className="text-login">EDYTOWANIE UŻYTKOWNIKA</h1>
                            <label>
                                Login:<br/>
                                <input type={"text"}
                                       name={"login"}
                                       onChange={e => setLogin(e.target.value)}
                                    placeholder={user.login}/>
                            </label>
                            <label>
                                Nowe hasło:<br/>
                                <input type={"password"}
                                       name={"password"}
                                       onChange={e => setPassword(e.target.value)}/>
                            </label><br/>
                            <label>
                                Imię:<br/>
                                <input type={"text"}
                                       name={"firstName"}
                                       onChange={e => setFirstName(e.target.value)}
                                placeholder={user.person.firstName}/>
                            </label>
                            <label>
                                Nazwisko:<br/>
                                <input type={"text"}
                                       name={"lastName"}
                                       onChange={e => setLastName(e.target.value)}
                                placeholder={user.person.lastName}/>
                            </label><br/>
                            <label>
                                Email:<br/>
                                <input type={"email"}
                                       name={"email"}
                                       onChange={e => setEmail(e.target.value)}
                                placeholder={user.person.email}/>
                            </label>
                            <label>
                                Data urodzenia:<br/>
                                <input type={"date"}
                                       name={"dateOfBirth"}
                                       onChange={e => setDateOfBirth(e.target.value)}
                                defaultValue={user.person.dateOfBirth.slice(0, 10)}/>
                            </label><br/>

                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button" onClick={handleSubmit}>ZAPISZ ZMIANY</button>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}