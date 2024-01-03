import Calendar from "react-calendar";
import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function AddInternalInvoice()
{
    const [choosenProducts, setChoosenProducts] = useState([])
    const [allProducts, setAllProducts] = useState([])
    const [isAddingFailed, setIsAddingFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("")
    const navigate=useNavigate()

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
                "payingDate": '',
                "clientUserId": '',
                "products": choosenProducts.map((product) => (
                    {
                        "name": product.name,
                        "quantity": product.choosenQuantity
                    }
                ))
            }
            const response = await mainBackendApi.post('Invoice/CreateInternalInvoice', data)
            navigate('/management/sprzedaz')
        } catch (e)
        {
            console.log(e)
        }
    }

    return (
        <>
            <div className="auth-page">
                <div className="container  justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">DODAWANIE WEWNĘTRZNEJ FAKTURY</h1>
                            <div>
                                <br/> PRODUKTY: <br/>
                                <div>
                                    <input
                                        type="text"
                                        list="productsList"
                                        onChange={handleProductChange}
                                    /><br/>
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
                                <br/>
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
                            <br/>
                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button"
                                        onClick={handleSubmit}>STWÓR WEWNĘTRZNĄ FAKTURĘ
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