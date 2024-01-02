import React, {useEffect, useState} from 'react';
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../../util/UserType";

export default function ReservationTable()
{

    const [reservations, setReservations] = React.useState([])
    const [currentPage, setCurrentPage] = useState(1);
    const [usersPerPage] = useState(100);
    const [noMoreUsers, setNoMoreUsers] = useState(false);
    const [isClient, setIsClient] = React.useState(true)

    useEffect(() =>
    {
        if (isUserLoggedIn() === true && getIsWorker() === false && getIsAdmin() === false)
        {
            setIsClient(true)
            fetchReservationsForClient()
        }
        else
        {
            setIsClient(false)
            fetchReservations()
        }
    }, []);


    async function fetchReservations()
    {
        try
        {
            const response = await mainBackendApi.get('/Reservation/GetAllReservations/' + usersPerPage + '/' + currentPage)
            const data = response.data
            if (data.length === 0)
            {
                setNoMoreUsers(true)
            }
            else
            {
                setNoMoreUsers(false)
                setReservations([...reservations, ...data])
                setCurrentPage(currentPage + 1);
            }
        } catch (e)
        {
            console.log(e)
        }
    }

    async function fetchReservationsForClient()
    {
        try
        {
            const response = await mainBackendApi.get('/Reservation/GetClientReservations/' + usersPerPage + '/' + currentPage)
            const data = response.data
            if (data.length === 0)
            {
                setNoMoreUsers(true)
            }
            else
            {
                setNoMoreUsers(false)
                setReservations([...reservations, ...data])
                setCurrentPage(currentPage + 1);
            }
        } catch (e)
        {
            console.log(e)
        }
    }

    return (
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
                    <tr key={reservation.id}>
                        <td>{reservation.id}</td>
                        <td>{reservation.startTime}</td>
                        <td>{reservation.endTime}</td>
                        <td>{reservation.lane.laneNumber}</td>
                        <td>{reservation.client.user.id} </td>
                        <td>{reservation.client.person.firstName} </td>
                        <td>{reservation.client.person.lastName} </td>
                    </tr>
                ))}
                </tbody>
            </table>
            <button onClick={(e) =>
            {
                e.preventDefault()
                isClient === true ? fetchReservationsForClient() : fetchReservations()
            }}>ZAŁADUJ DALEJ REZERWACJE
            </button>
            {(noMoreUsers === true) && <div className="no-more-users">Brak więcej rezerwacji</div>}
        </div>

    );
}