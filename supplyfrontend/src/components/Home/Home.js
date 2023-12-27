import './home.css';
import {useEffect, useState} from "react";
import {supplyBackendApi} from "../../util/Requests";


export default function Home()
{
    const [orders, setOrders] = useState([
        {
            id: 1,
            products: [
                {
                    name: "Produkt",
                    amount: 10
                }, {
                    name: "Wino",
                    amount: 15
                }
            ]

        }, {
            id: 2,
            products: [
                {
                    name: "Produkt",
                    amount: 10
                }, {
                    name: "Wino",
                    amount: 15
                }
            ]

        }
    ])

    useEffect(() =>
    {
        getOrders()
    }, [])

    async function getOrders()
    {
        try
        {
            const response = await supplyBackendApi.get('Order/GetUnfullfilledOrders')
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

    async function fullflillOrder(orderId)
    {
        try
        {
            const response = await supplyBackendApi.post('Order/FullfillOrder/' + orderId)
            console.log(response)
            if (response.status === 200)
            {
                const updatedOrders = orders.filter(order => order.id !== orderId)
                setOrders(updatedOrders)
            }
        } catch (error)
        {
            console.log(error)
        }
    }

    return (
        <>

            <div className="magazine-container">
                <div className="table-container">
                    <table className="table-bordered">
                        <thead>
                        <tr>
                            <th>ID ZAMÓWIENIA</th>
                            <th>PRODUKT</th>
                            <th>ILOŚĆ</th>
                            <th>AKCJA</th>
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
                                <td>
                                    <button onClick={e => fullflillOrder(order.id)}>SPEŁNIJ ZAMÓWIENIE</button>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    )
}