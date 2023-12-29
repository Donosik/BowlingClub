import "./Magazine_table.css"
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import React, {useEffect, useState} from "react";

export default function MagazineTable({filter})
{
    const [productsState, setProductsState] = useState([])
    const [currentPage, setCurrentPage] = useState(1);
    const [usersPerPage] = useState(100);
    const [noMoreProducts, setNoMoreProducts] = useState(false);

    useEffect(() =>
    {
        fetchProducts()
    }, []);

    async function fetchProducts()
    {
        try
        {
            const response = await mainBackendApi.get('TargetInventory/MagazineStatus/' + usersPerPage + '/' + currentPage)
            const data = response.data
            if (data.length === 0)
            {
                setNoMoreProducts(true)
            }
            else
            {
                setNoMoreProducts(false)
                setProductsState([...productsState, ...data])
                setCurrentPage(currentPage + 1);
            }
        } catch (error)
        {
            console.log(error)
        }
    }

    function handleEdit(id, field, value)
    {
        const numericValue = parseFloat(value)
        if (!isNaN(numericValue))
        {
            const newProducts = productsState.map((product) =>
            {
                if (product.id === id)
                {
                    return {...product, [field]: numericValue}
                }
                return product
            })
            setProductsState(newProducts)
        }
    }

    async function editProduct(id)
    {
        const product = productsState.find(product => product.id === id)
        try
        {
            console.log(product)
            const response = await mainBackendApi.put(`TargetInventory/${id}`, product)
        } catch (error)
        {
            console.log(error)
        }
    }

    async function deleteProduct(id)
    {
        try
        {
            const response = await mainBackendApi.delete(`TargetInventory/${id}`)
            const newProducts = productsState.filter(product => product.id !== id)
            setProductsState(newProducts)
        } catch (error)
        {
            console.log(error)
        }
    }

    return (
        <>

            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>Id Produktu</th>
                        <th>Nazwa produktu</th>
                        <th>Cena</th>
                        <th>Ilość docelowa</th>
                        <th>Ilość aktualna</th>
                        <th>Przyciski akcji</th>
                    </tr>
                    </thead>
                    <tbody>
                    {productsState.filter(product => product.name.toLowerCase().includes(filter.toLowerCase())).map((product, index) => (
                        <tr key={index}>
                            <td>{product.id}</td>
                            <td>{product.name}</td>
                            <td>
                                <input
                                    type="number"
                                    defaultValue={product.price}
                                    onChange={(e) => handleEdit(product.id, 'price', e.target.value)}
                                />
                            </td>
                            <td>
                                <input
                                    type="number"
                                    defaultValue={product.targetQuantity}
                                    onChange={(e) => handleEdit(product.id, 'quantity', e.target.value)}
                                />
                            </td>
                            <td>{product.currentQuantity}</td>
                            <td>
                                <button onClick={e => editProduct(product.id)}>ZAPISZ ZMIANY</button>
                                <button onClick={e => deleteProduct(product.id)}>USUŃ</button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
                <button onClick={fetchProducts}>ZAŁADUJ DALEJ UŻYTKOWNIKÓW</button>
                {(noMoreProducts === true) && <div className="no-more-users">Brak więcej użytkowników</div>}
            </div>
        </>
    )
}