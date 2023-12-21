import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function AddNewItem()
{
    const [name, setName] = useState('')
    const [price, setPrice] = useState('')
    const [targetAmount, setTargetAmount] = useState('')
    const navigate = useNavigate();
    const [isError, setIsError] = useState(false)
    const [errorMessage, setErrorMessage] = useState('')

    async function handleSubmit(e)
    {
        setIsError(false)
        e.preventDefault()
        if(price<0)
            return
        if(targetAmount<0)
            return
        try
        {
            const requestData = {
                "Name": name,
                "Price": price,
                "Quantity": targetAmount
            }
            const response = await mainBackendApi.post('TargetInventory', requestData)
            navigate('/management/magazyn')
        }
        catch (error)
        {
            setIsError(true)
            setErrorMessage("Błąd")
            console.log(error)
        }
    }
    return (
        <div className="magazine-container">
            <div className="table-name">MAGAZYN PRZEDMIOTÓW</div>
            <h1 >Dodaj nowy zakres produktów:</h1>
            <br/>
            {isError && <div className="error-message">{errorMessage}</div>}
            <label>
                Nazwa produktu:
                <input type={"text"} onChange={e=>setName(e.target.value)}/></label>
            <br/>
            <label>
                Cena:
                <input type={"number"} onChange={e=>setPrice(e.target.value)}/></label>
            <br/>
            <label>
                Ilość docelowa:
                <input type={"number"} onChange={e=>setTargetAmount(e.target.value)}/></label>
            <br/>
            <button onClick={handleSubmit}>DODAJ</button>
        </div>
    )
}