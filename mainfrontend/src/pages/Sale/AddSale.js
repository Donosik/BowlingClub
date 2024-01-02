import React, {useEffect, useState} from "react";
import Calendar from "react-calendar";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function AddSale()
{
    Date.prototype.addDays = function (days)
    {
        const date = new Date(this.valueOf());
        date.setDate(date.getDate() + days);
        return date;
    }
    const [choosenClient, setChoosenClient] = useState(null)
    const [choosenProducts, setChoosenProducts] = useState([])
    const [selectedDate, setSelectedDate] = useState(new Date().addDays(14))
    const [calendarVisible, setCalendarVisible] = useState(true)
    const [isAddingFailed, setIsAddingFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("")
    const [allClients, setAllClients] = useState([])
    const [allProducts, setAllProducts] = useState([])

    const navigate = useNavigate()

    useEffect(() =>
    {
        fetchAllProducts()
        fetchAllClients()
    }, [])

    async function fetchAllClients()
    {
        try
        {
            const response = await mainBackendApi.get('User/AllClients')
            setAllClients(response.data)
        } catch (e)
        {
            console.log(e)
        }
    }

    async function fetchAllProducts()
    {
        try
        {
            const response = await mainBackendApi.get('TargetInventory/MagazineStatus')
            const allProd = response.data
            const filteredProds = allProd.filter((product) => product.currentQuantity > 0)
            setAllProducts(filteredProds)
        } catch (e)
        {
            console.log(e)
        }
    }

    function handleClientChange(e)
    {
        const selectedValue = e.target.value
        const selectedClient = allClients.find((client) =>
        {
            const clientString = client.id + ". " + client.person.firstName + " " + client.person.lastName;
            return clientString === selectedValue;
        })
        setChoosenClient(selectedClient)
        e.target.value = ""
    }

    function handleProductChange(e)
    {
        const selectedValue = e.target.value
        const selectedProduct = allProducts.find((product) =>
        {
            return product.name === selectedValue;
        })
        setChoosenProducts([...choosenProducts, selectedProduct])
        e.target.value = ""
    }

    function removeProduct(product)
    {
        const updatedProducts = choosenProducts.filter((p) => p !== product);
        setChoosenProducts(updatedProducts);
    }

    async function handleSubmit()
    {
        try
        {
            const data = {
                "payingDate": selectedDate,
                "clientUserId": choosenClient.id,
                "products": choosenProducts.map((product) => (
                    {
                        "name": product.name,
                        "quantity": product.choosenQuantity
                    }
                ))
            }
            const response = await mainBackendApi.post('Invoice/CreateInvoice', data)
            navigate('/management/sprzedaz')
        } catch (e)
        {
            console.log(e)
        }
    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">DODAWANIE FAKTURY</h1>

                            <div className="calendar-container">
                                <button type="button"
                                        onClick={() => setCalendarVisible(!calendarVisible)}>
                                    {calendarVisible ? 'UKRYJ KALENDARZ DATY ZAPŁATY' : 'POKAŻ KALENDARZ DATY ZAPŁATY'}
                                </button>
                                {calendarVisible && (
                                    <>
                                        <Calendar
                                            value={selectedDate || new Date()}
                                            onChange={(date) => setSelectedDate(date)}
                                        />
                                        <br/>
                                    </>
                                )}
                            </div>
                            <div>
                                <label>Klient</label>
                                <input type="text"
                                       list="clients"
                                       onChange={handleClientChange}/>
                                <datalist id="clients">
                                    {allClients.map((client) => (
                                        <option key={client.id}
                                                value={client.id + ". " + client.person.firstName + " " + client.person.lastName}/>
                                    ))}
                                </datalist>
                                <div>
                                    {choosenClient && (choosenClient.id + ". " + choosenClient.person.firstName + " " + choosenClient.person.lastName)}
                                </div>
                            </div>

                            <div>
                                <label>Produkty</label>
                                <div>
                                    <input
                                        type="text"
                                        list="productsList"
                                        onChange={handleProductChange}
                                    />
                                    <datalist id="productsList">
                                        {allProducts.filter((product) => !choosenProducts.some((chosenProduct) => (chosenProduct.name === product.name)))
                                            .map((product) => (
                                                <option key={product.id}
                                                        value={product.name}/>
                                            ))}
                                    </datalist>
                                </div>
                                {allProducts.length > 0 && choosenProducts.length === allProducts.length && (
                                    <p>Brak dostępnych produktów do wyboru.</p>
                                )}
                                <div className="table-container">
                                <table className="table-bordered">
                                    <thead></thead>
                                    <tr>
                                        <th>NAZWA</th>
                                        <th>ILOŚĆ</th>
                                        <th>USUŃ</th>
                                    </tr>
                                    <tbody>
                                    {choosenProducts.map((product, index) => (
                                        <tr key={index}>
                                            <td>
                                                {product.name + " max(" + product.currentQuantity + ")"}</td>
                                            <td><input type={"number"}
                                                       max={product.currentQuantity}
                                                       min={1}
                                                       onChange={(e) =>
                                                       {
                                                           product.choosenQuantity = parseInt(e.target.value, 10)
                                                       }}/></td>
                                            <td>
                                                <button type="button"
                                                        onClick={() => removeProduct(product)}>
                                                    USUŃ
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                    </tbody>
                                </table>
                                </div>
                            </div>

                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button"
                                        onClick={handleSubmit}>STWÓRZ FAKTURĘ
                                </button>
                            </div>
                            {(isAddingFailed === true) && errorMessage}

                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}