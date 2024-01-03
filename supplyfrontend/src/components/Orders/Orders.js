import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {getIsLogged} from "../../util/UserUtil";
import {supplyBackendApi} from "../../util/Requests";

export default function Orders()
{
    const [orders, setOrders] = useState([])
    const navigate = useNavigate()
    useEffect(() =>
    {
        if (getIsLogged() == false)
        {    navigate('/Login')
            return
        }
        getOrders()
    }, []);

    async function getOrders()
    {
        try
        {
            const response = await supplyBackendApi.get('Order/MyOrders')
            console.log(response)
            const modifiedOrders = response.data.map(order => ({
                id: order.id,
                products: countProducts(order.products)
            }))
            setOrders(modifiedOrders)
        } catch (error)
        {
            console.log(error)
        }
    }

    function countProducts(products)
    {
        const occurrences = {}


        products.forEach((product) =>
        {
            occurrences[product.name] = occurrences[product.name] ? occurrences[product.name] + 1 : 1
        })

        const resultArray = Object.keys(occurrences).map((product) => ({
            name: product,
            amount: occurrences[product],
        }))

        return resultArray
    }

    return (
        <div className="magazine-container">
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>ID ZAMÓWIENIA</th>
                        <th>PRODUKT</th>
                        <th>ILOŚĆ</th>
                    </tr>
                    </thead>
                    <tbody>
                    {orders.map((order) => (
                        <tr key={order.id}>
                            <td>{order.id}</td>
                            <td>{order.products.map(product => (
                                <div>{product.name}</div>
                            ))}</td>
                            <td>{order.products.map(product => (
                                <div>{product.amount}</div>
                            ))}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}