import SaleListTable from "./SaleListTable";
import "./Sale.css"
import lupa from "../Magazine/Vector.png";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {getIsWorker} from "../../util/UserType";
import {mainBackendApi} from "../../util/Requests";
export default function Sale()
{
    const navigate=useNavigate()
    const [sales,setSales] = useState([
        {
            Id: 1,
            IssueDate: '1990-01-01',
            DueDate: '1990-01-01',
            Client:
                {
                    Id: 1,
                    FirstName: 'John',
                    LastName: 'Doe',
                },
            Worker:
                {
                    Id: 1,
                    FirstName: 'John',
                    LastName: 'Doe',
                },
            content: 'zawartosc'
        },
    ])
    const [isWorker,setIsWorker] = useState(false)

    useEffect(() =>
    {
        setIsWorker(getIsWorker())
        if(getIsWorker()===true)
        {
            fetchAllInvoices()
        }
        else
        {
            fetchOnlyClientInvoices()
        }
    }, []);

    async function fetchAllInvoices()
    {
        try
        {
            const response=await mainBackendApi.get('Invoice/GetAllInvoices')
            //console.log(response.data)
        }
        catch (e)
        {
            //console.log(e)
        }
    }

    async function fetchOnlyClientInvoices()
    {
        try
        {
            const response=await mainBackendApi.get('Invoice/ClientInvoices')
            //console.log(response.data)
        }
        catch (e)
        {
            //console.log(e)
        }
    }

    return (
        <>
            <div className="magazine-container">
           <div className="table-name"> FAKTURY I SPRZEDAŻE</div>
                <input/><img src={lupa} alt="lupa"/>
                {(isWorker===true)&&<button onClick={()=>navigate('dodaj')}>ZAREJESTRUJ SPRZEDAŻ</button>}
                <br/><SaleListTable sales={sales}/></div>
        </>
    )
}