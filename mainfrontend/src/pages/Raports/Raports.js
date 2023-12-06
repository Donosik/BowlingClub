import MostWorkedHours from "./MostWorkedHours";
import BestBuyingClient from "./BestBuyingClient";
import BestSellingProduct from "./BestSellingProduct";
import {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function Raports()
{

    const [workersWithHours, setWorkersWithHours] = useState([
        {
            id: 1,
            fullName: 'John Doe',
            email: '',
            totalWorkHours: 10,
        },
        {
            id: 2,
            fullName: 'John Doe',
            email: '',
            totalWorkHours: 10,
        },
        {
            id: 3,
            fullName: 'John Doe',
            email: '',
            totalWorkHours: 10,
        },
        {
            id: 4,
            fullName: 'John Doe',
            email: '',
            totalWorkHours: 10,
        },
        {
            id: 5,
            fullName: 'John Doe',
            email: '',
            totalWorkHours: 10,
        },
    ])
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

    useEffect(() =>{
        fetchWorkersWithHours()
    }, [])

    async function fetchWorkersWithHours()
    {
        try
        {
            const response = await mainBackendApi.get('/Raport/MostWorkedHours/' + howManyDaysAgo + '/' + howManyDaysForward + '/' + howManyTop)
            const data = response.data
            setWorkersWithHours(data)
        } catch (error)
        {
            console.log(error)
        }
    }

    return (<>

            <div className="table-container">
                <form>
                    <MostWorkedHours workersWithHours={workersWithHours}/>
                </form>
            </div>
            <br/>
            <div className="table-container">
                <form><BestBuyingClient buyingClients={buyingClient}/></form>
            </div>

            <br/>
            <div className="table-container">
                <form><BestSellingProduct sellingProducts={sellingProducts}/></form>
            </div>
        </>)
}