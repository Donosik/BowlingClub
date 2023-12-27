import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function AddNewItem() {
    const [name, setName] = useState('')
    const [price, setPrice] = useState('')
    const [targetAmount, setTargetAmount] = useState('')
    const navigate = useNavigate();
    const [isError, setIsError] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    async function handleSubmit(e) {
        setIsError(false)
        e.preventDefault()
        if (price < 0)
            return
        if (targetAmount < 0)
            return
        try {
            const requestData = {
                "Name": name,
                "Price": price,
                "Quantity": targetAmount
            }
            const response = await mainBackendApi.post('TargetInventory', requestData)
            navigate('/management/magazyn')
        } catch (error) {
            setIsError(true)
            setErrorMessage("Błąd")
            console.log(error)
        }
    }

    return (
        <div className="magazine-container">
            <div className="container page d-flex justify-content-center align-items-center">
                <div className="row">
                    <div className="login-box">

                        <h1 className="text-login">DODAWANIE PRODUKTÓW</h1>
                        <br/>
                        <label>
                            NAZWA PRODUKTU:<br/>
                            <input type={"text"} onChange={e => setName(e.target.value)}/></label>
                        <br/>
                        <label>
                            CENA W PLN:<br/>
                            <input type={"number"} onChange={e => setPrice(e.target.value)}/></label>
                        <br/>
                        <label>
                            ILOŚĆ, JAKA POWINNA BYĆ W MAGAZYNIE:<br/>
                            <input type={"number"} onChange={e => setTargetAmount(e.target.value)}/></label>
                        <br/>
                        {isError && <div className="error-message">{errorMessage}</div>}
                        <button onClick={handleSubmit}>DODAJ</button>
                    </div>
                </div>
            </div>
        </div>
    )
}