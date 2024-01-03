import React, {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import './profile.css';
export default function Profile()
{
    const [login, setLogin] = useState("")
    const [password, setPassword] = useState("")
    const [firstName, setFirstName] = useState("")
    const [lastName, setLastName] = useState("")
    const [email, setEmail] = useState("")
    const [dateOfBirth, setDateOfBirth] = useState("")
    const [isActive, setIsActive] = useState(false)
    const [user, setUser] = useState(
        {
            id:'',
            login:'',
            person:{
                firstName:'',
                lastName:'',
                email:'',
                dateOfBirth:''
            }
        }
    )
    const [isRegisterFailed, setIsRegisterFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("")

    useEffect(() =>
    {
        fetchMe()
    }, []);

    async function fetchMe()
    {
        try
        {
            const response = await mainBackendApi.get('User/GetMe')
            setUser(response.data)
            setLogin(response.data.login)
            setPassword(response.data.password)
            setFirstName(response.data.person.firstName)
            setLastName(response.data.person.lastName)
            setEmail(response.data.person.email)
            setDateOfBirth(response.data.person.dateOfBirth)
            setIsActive(response.data.isActive)
        } catch (e)
        {
            console.log(e)
        }
    }

    const validateEmail = (email) =>
    {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const validateLettersOnly = (text) =>
    {
        const lettersOnlyRegex = /^[A-Za-z]+$/;
        return lettersOnlyRegex.test(text);
    };

    const validateLogin = (login) =>
    {
        // Login musi mieć min. 3 znaki, może zawierać cyfry, ale nie znaki specjalne
        const loginRegex = /^[A-Za-z0-9]{3,}$/;
        return loginRegex.test(login);
    };

    const validatePassword = (password) =>
    {
        // Hasło musi zawierać co najmniej 8 znaków, jedną cyfrę i jeden znak specjalny
        const passwordRegex = /(?=.*\d)(?=.*[@\-!]).{8,}/;
        return passwordRegex.test(password);
    };

    const handleSubmit = async (e) =>
    {
        e.preventDefault();

        setIsRegisterFailed(false);
        setErrorMessage("");

        if (
            !login.trim() ||
            !firstName.trim() ||
            !lastName.trim() ||
            !email.trim() ||
            !dateOfBirth.trim()
        )
        {
            setIsRegisterFailed(true);
            setErrorMessage("Wszystkie pola muszą być wypełnione");
            return;
        }

        if (!validateLogin(login))
        {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Login musi mieć minimum 3 znaki i nie może zawierać znaków specjalnych"
            );
            return;
        }

        if (password.trim() && !validatePassword(password))
        {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Hasło musi mieć minimum 8 znaków, jedną cyfrę i jeden znak specjalny"
            );
            return;
        }

        if (!validateLettersOnly(firstName))
        {
            setIsRegisterFailed(true);
            setErrorMessage("Imię może zawierać tylko litery");
            return;
        }

        if (!validateLettersOnly(lastName))
        {
            setIsRegisterFailed(true);
            setErrorMessage("Nazwisko może zawierać tylko litery");
            return;
        }

        if (!validateEmail(email))
        {
            setIsRegisterFailed(true);
            setErrorMessage("Niepoprawny format adresu e-mail");
            return;
        }

        try
        {
            const requestData = {
                login: login,
                password: password,
                firstName: firstName,
                lastName: lastName,
                email: email,
                dateOfBirth: dateOfBirth,
                isActive: isActive
            };
            const response = await mainBackendApi.put('User/ChangeMe', requestData)
            setIsRegisterFailed(true)
            setErrorMessage("Pomyślnie zmieniono dane")
        } catch (error)
        {
            setIsRegisterFailed(true);
            setErrorMessage("Błąd");
            console.log(error);
        }
    };

    return (
        <div className="auth-page">
            <div className="container page d-flex justify-content-center align-items-center">
                <div className="row">
                    <div className="form-box">
                        <h1 className="text-login">EDYCJA PROFILU UŻYTKOWNIKA</h1>
                        <p>Instrukcja:</p>
                        <ul>
                            <li>Login musi mieć min. 3 znaki i nie może zawierać znaków specjalnych.</li>
                            <li>Hasło musi zawierać minimalnie: 8 znaków, jedną cyfrę oraz znak specjalny (np.
                                @-!).
                            </li>
                            <li>ID, które jest poniżej służy do wystawiania faktur - podaj je pracowikowi, kiedy prosisz o wystawienie faktury.
                            </li>
                        </ul>

                        <br/>
                        <div className="id-edit-prof">

                            PROFIL:
                        </div>
                        <label>
                            Login:
                            <br/>
                            <input
                                type={"text"}
                                name={"login"}
                                onChange={(e) => setLogin(e.target.value)}
                                placeholder={user.login}
                            />
                        </label>
                        <label>
                            Nowe hasło:
                            <br/>
                            <input
                                type={"password"}
                                name={"password"}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </label>
                        <br/>
                        <label>
                            Imię:
                            <br/>
                            <input
                                type={"text"}
                                name={"firstName"}
                                onChange={(e) => setFirstName(e.target.value)}
                                placeholder={user.person.firstName}
                            />
                        </label>
                        <label>
                            Nazwisko:
                            <br/>
                            <input
                                type={"text"}
                                name={"lastName"}
                                onChange={(e) => setLastName(e.target.value)}
                                placeholder={user.person.lastName}
                            />
                        </label>
                        <br/>
                        <label>
                            Email:
                            <br/>
                            <input
                                type={"email"}
                                name={"email"}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder={user.person.email}
                            />
                        </label>
                        <label>
                            Data urodzenia:
                            <br/>
                            <input
                                type={"date"}
                                name={"dateOfBirth"}
                                onChange={(e) => setDateOfBirth(e.target.value)}
                                defaultValue={user.person.dateOfBirth.slice(0, 10)}
                            />
                        </label>

                        <div className="d-flex justify-content-center align-items-center">
                            <button type="button"
                                    onClick={handleSubmit}>
                                ZAPISZ ZMIANY
                            </button>
                        </div>

                        {isRegisterFailed ? (
                            <div className="error-message">{errorMessage}</div>
                        ) : null}
                        <div className="id-edit-prof">

                            TWOJE ID W SYSTEMIE: {user.id}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}