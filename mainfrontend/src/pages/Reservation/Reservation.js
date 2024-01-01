import ReservationTable from "./ReservationTable";
import React from "react";

export default function Reservation()
{
    const reservations = [
        {
            Id: 1,
            StartTime: '2023-11-08T09:00:00',
            EndTime: '2023-11-08T10:00:00',
            LaneNumber: 3,
            Client: {
                Id: 1,
                FirstName: 'John',
                LastName: 'Doe',
            },
        },
        {
            Id: 2,
            StartTime: '2023-11-08T10:30:00',
            EndTime: '2023-11-08T11:30:00',
            LaneNumber: 5,
            Client: {
                Id: 2,
                FirstName: 'Jane',
                LastName: 'Smith',
            },
        },
        // Dodaj więcej przykładowych rezerwacji tutaj...
    ];

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZARZĄDZANIE REZERWACJAMI</div>
                    <button>DODAJ REZERWACJE</button>
                    <ReservationTable reservations={reservations}/>
                </form>
            </div>
        </>
    )
}