import ReservationTable from "./ReservationTable";
import React, {useEffect} from "react";
import {useNavigate} from "react-router-dom";
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../../util/UserType";

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
    ];
    const [isClient, setIsClient] = React.useState(false)
    const navigate = useNavigate()

    useEffect(() =>
    {
        if (isUserLoggedIn() === true && getIsWorker() === false && getIsAdmin() === false)
            setIsClient(true)
        else
            setIsClient(false)
    }, []);

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZARZĄDZANIE REZERWACJAMI</div>

                    {(isClient === true) && <button onClick={() => navigate('dodaj')}>DODAJ REZERWACJE</button>}
                    <ReservationTable reservations={reservations}/>
                </form>
            </div>
        </>
    )
}