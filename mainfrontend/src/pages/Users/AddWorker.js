import {useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function AddWorker()
{
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
            const response = await mainBackendApi.post('User/RegisterWorker', requestData)
            console.log(response)
        } catch (error)
        {
            setIsRegisterFailed(true)
            setErrorMessage("Błąd")
            console.log(error)
        }

    }

    return (
        <>
            Formularz dodawania pracownika
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
                Imię:
                <input type={"text"}
                       name={"firstName"}
                       onChange={e => setFirstName(e.target.value)}/>
            </label>
            <label>
                Nazwisko:
                <input type={"text"}
                       name={"lastName"}
                       onChange={e => setLastName(e.target.value)}/>
            </label>
            <label>
                Email:
                <input type={"email"}
                       name={"email"}
                       onChange={e => setEmail(e.target.value)}/>
            </label>
            <label>
                Data urodzenia:
                <input type={"date"}
                       name={"dateOfBirth"}
                       onChange={e => setDateOfBirth(e.target.value)}/>
            </label>
            <input type={"button"}
                   value={"Zarejestruj pracownika"}
                   onClick={handleSubmit}/>
            {isRegisterFailed ? errorMessage : null}
        </>
    )
}