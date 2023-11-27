import React from 'react';

export default function ReservationTable({reservations}) {
    return (

        <div className="table-container">
            <div className="table-name">ZARZĄDZANIE REZERWACJAMI</div>
            <div className="table-container">
            <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Początek rezerwacji</th>
                        <th>Koniec rezerwacji</th>
                        <th>Tor</th>
                        <th>Id klienta</th>
                        <th>Imię klienta</th>
                        <th>Nazwisko klienta</th>
                    </tr>
                    </thead>
                    <tbody>
                    {reservations.map(reservation => (
                        <tr key={reservation.Id}>
                            <td>{reservation.Id}</td>
                            <td>{reservation.StartTime}</td>
                            <td>{reservation.EndTime}</td>
                            <td>{reservation.LaneNumber}</td>
                            <td>{reservation.Client.Id} </td>
                            <td>{reservation.Client.FirstName} </td>
                            <td>{reservation.Client.LastName} </td>
                        </tr>
                    ))}
                    </tbody>
                </table></div></div>

    );
}