import React from "react";

export default function MostWorkedHours({workersWithHours})
{
    return(
        <div className="table-container">
            <div className="table-name">RAPORTY</div><br/><br/>
            <div className="table-name">
                Najwiecej przepracowanych godzin</div>
            <div className="table-container">
                <table className="table-bordered">
            <thead>
            <tr>
                <th>ID</th>
                <th>Imię i nazwisko</th>
                <th>Email</th>
                <th>Liczba godzin</th>
            </tr>
            </thead>
            <tbody>
            {workersWithHours.map((worker)=>(
                <tr key={worker.Id}>
                    <td>{worker.Id}</td>
                    <td>{worker.FullName}</td>
                    <td>{worker.Email}</td>
                    <td>{worker.Hours}</td>
                </tr>
            ))}
            </tbody>
                </table></div></div>
    )
}