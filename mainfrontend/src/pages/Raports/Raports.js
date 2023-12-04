import MostWorkedHours from "./MostWorkedHours";
import BestBuyingClient from "./BestBuyingClient";
import BestSellingProduct from "./BestSellingProduct";
import {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function Raports()
{
    const buyingClient = [{
        Id: 1,
        FullName: 'John Doe',
        Email: 'john@example.com',
        Invoices: 10,
    },
        {
            Id: 2,
            FullName: 'John Dupa',
            Email: 'john1@example.com',
            Invoices: 150,
        }
        ,
        {
            Id: 2,
            FullName: 'John Dupa',
            Email: 'john1@example.com',
            Invoices: 150,
        }
        ,
        {
            Id: 2,
            FullName: 'John Dupa',
            Email: 'john1@example.com',
            Invoices: 150,
        }
        ,
        {
            Id: 2,
            FullName: 'John Dupa',
            Email: 'john1@example.com',
            Invoices: 150,
        }
        ,
        {
            Id: 2,
            FullName: 'John Dupa',
            Email: 'john1@example.com',
            Invoices: 150,
        }

    ]

    const sellingProducts = [{
        Id: 1,
        ProductName: 'Drink',
        Sold: 1500,
    },]

    return (
        <>

            <div className="table-container">
                <form>
                    <MostWorkedHours/>
                </form>
            </div>
            <br/>
            {
                /*
            <div className="table-container">
                <form><BestBuyingClient buyingClients={buyingClient}/></form>
            </div>

            <br/>
            <div className="table-container">
                <form><BestSellingProduct sellingProducts={sellingProducts}/></form>
            </div>*/
            }
        </>
    )
}