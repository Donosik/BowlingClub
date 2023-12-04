import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function AddRegulation()
{
    const [number, setNumber] = useState()
    const [description, setDescription] = useState('')
    const [errorMessage, setErrorMessage] = useState('')
    const [isAddingFailed, setIsAddingFailed] = useState(false)
    const navigate = useNavigate()

    async function handleSubmit()
    {
        setIsAddingFailed(false)
        try
        {
            const requestData = {
                "number": number,
                "description": description
            }
            const response = await mainBackendApi.post('Regulation', requestData)
            console.log(response)
            navigate('../')
        } catch (error)
        {
            setIsAddingFailed(true)
            setErrorMessage('Punkt regulaminu o podanym numerze już istnieje')
        }
    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center min-vh-100">
                    <div className="row">
                        <div className="login-box">
                            <h1 className="text-login">DODAWANIE PUNKTU REGULAMINU</h1>
                            Punkt:
                            <input type={"number"}
                                   name={"number"}
                                   onChange={e => setNumber(e.target.value)}/>
                            <br/>
                            Opis:
                            <input type={"text"}
                                   name={"description"}
                                   onChange={e => setDescription(e.target.value)}/>
                            <div className="d-flex justify-content-center align-items-center">
                                <button type="submit"
                                        onClick={handleSubmit}>DODAJ PUNKT REGULAMINU
                                </button>
                            </div>
                            {isAddingFailed ? <div className="error-message">{errorMessage}</div> : null}
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}