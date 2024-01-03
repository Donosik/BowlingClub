import React, { useState } from "react";
import { mainBackendApi } from "../../util/Requests";
import "./login.css";
import {GoogleLogin} from "@react-oauth/google";
import {jwtDecode} from "jwt-decode";

export default function RegisterForm(props) {
    const { loginCallback } = props;

    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState("");
    const [isRegisterFailed, setIsRegisterFailed] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    async function handleSubmit(e) {
        e.preventDefault();

        setIsRegisterFailed(false);
        setErrorMessage("");

        // Validation logic
        if (login.length < 3 || !/^[a-zA-Z0-9]+$/.test(login)) {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Nieprawidłowy login."
            );
            return;
        }

        if (!/(?=.*\d)(?=.*[@\-!]).{8,}/.test(password)) {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Nieprawidłowe hasło."
            );
            return;
        }

        if (!/^[a-zA-Z]+$/.test(firstName)) {
            setIsRegisterFailed(true);
            setErrorMessage("Nieprawidłowe imię. Imię może zawierać tylko litery.");
            return;
        }

        if (!/^[a-zA-Z]+$/.test(lastName)) {
            setIsRegisterFailed(true);
            setErrorMessage("Nieprawidłowe nazwisko. Nazwisko może zawierać tylko litery.");
            return;
        }

        if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
            setIsRegisterFailed(true);
            setErrorMessage("Nieprawidłowy email. Podaj poprawny adres email.");
            return;
        }

        const currentDate = new Date();
        const selectedDate = new Date(dateOfBirth);

        if (!dateOfBirth || selectedDate > currentDate) {
            setIsRegisterFailed(true);
            setErrorMessage("Nieprawidłowa data urodzenia. Podaj poprawną datę urodzenia.");
            return;
        }

        try {
            const reguestData = {
                login: login,
                password: password,
                firstName: firstName,
                lastName: lastName,
                email: email,
                dateOfBirth: dateOfBirth,
            };

            const response = await mainBackendApi.post("User/RegisterClient", reguestData);
            loginCallback()
        } catch (error) {
            setIsRegisterFailed(true);
            setErrorMessage("ISTNIEJE JUŻ KONTO Z TYM ADRESEM E-MAIL");
            console.log(error);
        }
    }

    function handleLogin() {
        if (loginCallback) loginCallback();
    }

    async function successResponse(response)
    {
        const userObject = jwtDecode(response.credential)
        const googleForm=
            {
                "email":userObject.email,
                "firstName":userObject.given_name,
                "lastName":userObject.family_name
            }
        try{
            const response=await mainBackendApi.post('User/RegisterClientGoogle',googleForm)
            console.log(response)
        }
        catch (e)
        {
            console.log(e)
        }
    }

    function errorResponse(response)
    {
        console.log(response)
    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">REJESTRACJA</h1>
                            <form>
                                Instrukcja:
                                <li>
                                    {" "}
                                    Login musi mieć min. 3 znaki i nie może zawierać znaków
                                    specjalnych.
                                </li>
                                <li>
                                    {" "}
                                    Hasło musi zawierać minimalnie: 8 znaków, jedną cyfrę oraz
                                    znak specjalny (np. @-!).
                                </li>
                                <div className="error-message">
                                    {isRegisterFailed ? errorMessage : null}</div>
                                <br/>
                                <label>
                                    Login:<br/>
                                    <input
                                        type={"text"}
                                        name={"login"}
                                        onChange={(e) => setLogin(e.target.value)}
                                    />
                                </label>
                                <label>
                                    Hasło:<br/>
                                    <input
                                        type={"password"}
                                        name={"password"}
                                        onChange={(e) => setPassword(e.target.value)}
                                    />
                                </label>{" "}
                                <br/>
                                <label>
                                    Imię:<br/>
                                    <input
                                        type={"text"}
                                        name={"firstName"}
                                        onChange={(e) => setFirstName(e.target.value)}
                                    />
                                </label>
                                <label>Nazwisko: <br/>
                                    <input
                                        type={"text"}
                                        name={"lastName"}
                                        onChange={(e) => setLastName(e.target.value)}
                                    />
                                </label>
                                <br/>
                                <label>
                                    Email:<br/>
                                    <input
                                        type={"email"}
                                        name={"email"}
                                        onChange={(e) => setEmail(e.target.value)}
                                    />
                                </label>
                                <label>
                                    Data urodzenia:<br/>
                                    <input
                                        type={"date"}
                                        name={"dateOfBirth"}
                                        onChange={(e) => setDateOfBirth(e.target.value)}
                                    />
                                </label>
                                <br/>

                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="submit"
                                            onClick={handleSubmit}>
                                        ZAREJESTRUJ SIĘ
                                    </button>
                                </div>
                                <div className="d-flex justify-content-center align-items-center">
                                    <button type="button"
                                            onClick={handleLogin}>
                                        ZALOGUJ SIĘ
                                    </button>
                                </div>
                                <div className="d-flex justify-content-center align-items-center">
                                    <GoogleLogin onSuccess={successResponse}
                                                 onError={errorResponse}/>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
