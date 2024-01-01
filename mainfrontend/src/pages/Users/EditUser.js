import { useParams } from "react-router-dom";
import React, { useState } from "react";
import { mainBackendApi } from "../../util/Requests";

export default function EditUser() {
    const { id } = useParams();

    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState("");
    const [isRegisterFailed, setIsRegisterFailed] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");

    const validateEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const validateLettersOnly = (text) => {
        const lettersOnlyRegex = /^[A-Za-z]+$/;
        return lettersOnlyRegex.test(text);
    };

    const validateLogin = (login) => {
        // Login musi mieć min. 3 znaki, może zawierać cyfry, ale nie znaki specjalne
        const loginRegex = /^[A-Za-z0-9]{3,}$/;
        return loginRegex.test(login);
    };

    const validatePassword = (password) => {
        // Hasło musi zawierać co najmniej 8 znaków, jedną cyfrę i jeden znak specjalny
        const passwordRegex = /(?=.*\d)(?=.*[@\-!]).{8,}/;
        return passwordRegex.test(password);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setIsRegisterFailed(false);
        setErrorMessage("");

        if (
            !login.trim() ||
            !firstName.trim() ||
            !lastName.trim() ||
            !email.trim() ||
            !dateOfBirth.trim()
        ) {
            setIsRegisterFailed(true);
            setErrorMessage("Wszystkie pola muszą być wypełnione");
            return;
        }

        if (!validateLogin(login)) {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Login musi mieć minimum 3 znaki i nie może zawierać znaków specjalnych"
            );
            return;
        }

        if (password.trim() && !validatePassword(password)) {
            setIsRegisterFailed(true);
            setErrorMessage(
                "Hasło musi mieć minimum 8 znaków, jedną cyfrę i jeden znak specjalny"
            );
            return;
        }

        if (!validateLettersOnly(firstName)) {
            setIsRegisterFailed(true);
            setErrorMessage("Imię może zawierać tylko litery");
            return;
        }

        if (!validateLettersOnly(lastName)) {
            setIsRegisterFailed(true);
            setErrorMessage("Nazwisko może zawierać tylko litery");
            return;
        }

        if (!validateEmail(email)) {
            setIsRegisterFailed(true);
            setErrorMessage("Niepoprawny format adresu e-mail");
            return;
        }

        try {
            const requestData = {
                login: login,
                password: password,
                firstName: firstName,
                lastName: lastName,
                email: email,
                dateOfBirth: dateOfBirth,
            };
            const response = await mainBackendApi.post(
                "User/RegisterWorker",
                requestData
            );
            console.log(response);
        } catch (error) {
            setIsRegisterFailed(true);
            setErrorMessage("Błąd");
            console.log(error);
        }
    };

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">EDYTOWANIE UŻYTKOWNIKA</h1>
                            <p>Instrukcja:</p>
                            <ul>
                                <li>Login musi mieć min. 3 znaki i nie może zawierać znaków specjalnych.</li>
                                <li>Hasło musi zawierać minimalnie: 8 znaków, jedną cyfrę oraz znak specjalny (np. @-!).</li>
                            </ul>
                            <label>
                                Login:
                                <br />
                                <input
                                    type={"text"}
                                    name={"login"}
                                    onChange={(e) => setLogin(e.target.value)}
                                />
                            </label>
                            <label>
                                Nowe hasło:
                                <br />
                                <input
                                    type={"password"}
                                    name={"password"}
                                    onChange={(e) => setPassword(e.target.value)}
                                />
                            </label>
                            <br />
                            <label>
                                Imię:
                                <br />
                                <input
                                    type={"text"}
                                    name={"firstName"}
                                    onChange={(e) => setFirstName(e.target.value)}
                                />
                            </label>
                            <label>
                                Nazwisko:
                                <br />
                                <input
                                    type={"text"}
                                    name={"lastName"}
                                    onChange={(e) => setLastName(e.target.value)}
                                />
                            </label>
                            <br />
                            <label>
                                Email:
                                <br />
                                <input
                                    type={"email"}
                                    name={"email"}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </label>
                            <label>
                                Data urodzenia:
                                <br />
                                <input
                                    type={"date"}
                                    name={"dateOfBirth"}
                                    onChange={(e) => setDateOfBirth(e.target.value)}
                                />
                            </label>
                            <br />

                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button" onClick={handleSubmit}>
                                    ZAPISZ ZMIANY
                                </button>
                            </div>

                            {isRegisterFailed ? (
                                <div className="error-message">{errorMessage}</div>
                            ) : null}
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
