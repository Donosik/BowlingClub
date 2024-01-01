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
        // Prosta walidacja adresu e-mail, można dostosować zgodnie z potrzebami
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const validateDateOfBirth = (date) => {
        // Prosta walidacja daty urodzenia, można dostosować zgodnie z potrzebami
        const dateOfBirthRegex = /^\d{4}-\d{2}-\d{2}$/;
        return dateOfBirthRegex.test(date);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setIsRegisterFailed(false);
        setErrorMessage("");

        // Walidacja imienia
        if (firstName.trim() === "") {
            setIsRegisterFailed(true);
            setErrorMessage("Imię nie może być puste");
            return;
        }

        // Walidacja nazwiska
        if (lastName.trim() === "") {
            setIsRegisterFailed(true);
            setErrorMessage("Nazwisko nie może być puste");
            return;
        }

        // Walidacja adresu e-mail
        if (!validateEmail(email)) {
            setIsRegisterFailed(true);
            setErrorMessage("Niepoprawny format adresu e-mail");
            return;
        }

        // Walidacja daty urodzenia
        if (!validateDateOfBirth(dateOfBirth)) {
            setIsRegisterFailed(true);
            setErrorMessage("Niepoprawny format daty urodzenia");
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
