import BestBuyingClient from "./BestBuyingClient"
import BestSellingProduct from "./BestSellingProduct"
import {useEffect, useState} from "react"
import {mainBackendApi} from "../../util/Requests"

export default function Raports()
{
    const [buyingClient, setBuyingClient] = useState([
        {
            Id: 1, FullName: 'John Doe', Email: 'john@example.com', Invoices: 10,
        }, {
            Id: 2, FullName: 'John Dupa', Email: 'john1@example.com', Invoices: 150,
        }, {
            Id: 3, FullName: 'John Dupa', Email: 'john1@example.com', Invoices: 150,
        }, {
            Id: 4, FullName: 'John Dupa', Email: 'john1@example.com', Invoices: 150,
        }, {
            Id: 5, FullName: 'John Dupa', Email: 'john1@example.com', Invoices: 150,
        }, {
            Id: 6, FullName: 'John Dupa', Email: 'john1@example.com', Invoices: 150,
        }

    ])
    const [sellingProducts, setSellingProducts] = useState([{
        Id: 1, ProductName: 'Drink', Sold: 1500,
    },])

    const [howManyDaysAgo, setHowManyDaysAgo] = useState(7)
    const [howManyDaysForward, setHowManyDaysForward] = useState(30)
    const [howManyTop, setHowManyTop] = useState(5)

    useEffect(() =>
    {
        calculateDays()
        fetchSellingProducts()
        fetchBuyingClients()
    }, [])

    function calculateDays()
    {
        const currentDate = new Date()
        const firstDayOfMonth = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1)
        const lastDayOfMonth = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0)

        const daysInMonth = lastDayOfMonth.getDate()
        const daysAgo = daysInMonth - currentDate.getDate()
        const daysForward = daysInMonth - daysAgo

        setHowManyDaysAgo(daysAgo)
        setHowManyDaysForward(daysForward)
        setHowManyTop(5)
    }

    async function fetchBuyingClients()
    {
        try
        {
            const response = await mainBackendApi.get('/Raport/BestBuyingClient/' + howManyDaysAgo + '/' + howManyDaysForward + '/' + howManyTop)
            const data = response.data
            setBuyingClient(data)
        } catch (error)
        {
            console.log(error)
        }
    }

    async function fetchSellingProducts()
    {
        try
        {
            const response = await mainBackendApi.get('/Raport/BestSellingProducts/' + howManyDaysAgo + '/' + howManyDaysForward + '/' + howManyTop)
            const data = response.data
            setSellingProducts(data)
        } catch (error)
        {
            console.log(error)
        }
    }

    return (
        <>
            <div className="table-container">
                <form><BestBuyingClient buyingClients={buyingClient}/></form>
            </div>

            <br/>
            <div className="table-container">
                <form><BestSellingProduct sellingProducts={sellingProducts}/></form>
            </div>
        </>
    )
}