import SaleListTable from "./SaleListTable";
import "./Sale.css"
import lupa from "../Magazine/Vector.png";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {getIsWorker} from "../../util/UserType";
import {mainBackendApi} from "../../util/Requests";

export default function Sale()
{
    const navigate = useNavigate()
    const [sales, setSales] = useState([])
    const [isWorker, setIsWorker] = useState(false)
    const [isInternal, setIsInternal] = useState(false)

    useEffect(() =>
    {
        setIsWorker(getIsWorker())
        if (getIsWorker() === true)
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
            if (isInternal == true)
            {
                const response=await mainBackendApi.get('Invoice/AllInternalInvoices')
                setSales(response.data)
            }
            else
            {
                const response = await mainBackendApi.get('Invoice/AllInvoices')
                setSales(response.data)
            }
        } catch (e)
        {
            console.log(e)
        }
    }

    async function fetchOnlyClientInvoices()
    {
        try
        {
            const response = await mainBackendApi.get('Invoice/ClientInvoices')
            console.log(response.data)
        } catch (e)
        {
            console.log(e)
        }
    }

    return (
        <>
            <div className="magazine-container">
                <div className="table-name"> FAKTURY I SPRZEDAŻE</div>
                <input type={"text"}/><img src={lupa}
                                           alt="lupa"/>
                {(isWorker === true) && (
                    <>
                        <button onClick={() => navigate('dodaj')}>ZAREJESTRUJ SPRZEDAŻ
                        </button>
                        <button onClick={() => navigate('wewnetrzna')}>STWÓRZ WEWNĘTRZNĄ FAKTURĘ
                        </button>
                        <br/>
                        <input type={"checkbox"}
                               checked={isInternal}
                               onChange={(e) => setIsInternal(e.target.checked)}/> Wewnętrzne faktury
                    </>
                )}
                <br/><SaleListTable sales={sales}/></div>

        </>
    )
}