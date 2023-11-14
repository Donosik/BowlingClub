import SaleListTable from "./SaleListTable";
import "./Sale.css"
import lupa from "../Magazine/Vector.png";
import React from "react";
export default function Sale()
{
    const sales = [
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
    ]

    return (
        <>
            <div className="magazine-container">
           <div className="table-name"> FAKTURY I SPRZEDAŻE</div>
                <input/><img src={lupa} alt="lupa"/><button>ZAREJESTRUJ SPRZEDAŻ
            </button><br/><SaleListTable sales={sales}/></div>
        </>
    )
}