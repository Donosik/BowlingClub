import React from "react";

export default function BestBuyingClient({buyingClients})
{
    return(
        <div className="table-container">
            <div className="table-name">
                Najlepsi klienci</div>
            <div className="table-container">
                <table className="table-bordered">
            <thead>
            <tr>
                <th>ID</th>
                <th>Imię i nazwisko</th>
                <th>Email</th>
                <th>Liczba faktur</th>
            </tr>
            </thead>
            <tbody>
            {buyingClients.map((client)=>(
                <tr key={client.Id}>
                    <td>{client.Id}</td>
                    <td>{client.FullName}</td>
                    <td>{client.Email}</td>
                    <td>{client.Invoices}</td>
                </tr>
            ))}
            </tbody>
            </table></div></div>
    )
}