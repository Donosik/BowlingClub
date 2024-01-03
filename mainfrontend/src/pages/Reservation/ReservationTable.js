import React, {useEffect, useState} from 'react';
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../../util/UserType";

export default function ReservationTable()
{
    const [reservations, setReservations] = useState([])
    const [currentPage, setCurrentPage] = useState(1);
    const [usersPerPage] = useState(100);
    const [noMoreUsers, setNoMoreUsers] = useState(false);
    const [isClient, setIsClient] = useState(true)
    const [onlyNewReservations, setOnlyNewReservations] = useState(true);
    const [onlyUnrealizedReservations, setOnlyUnrealizedReservations] = useState(getIsWorker());
    const [shouldFetch, setShouldFetch] = useState(false);
    const navigate=useNavigate()

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

    useEffect(() =>
    {
        if (shouldFetch)
        {
            if (isClient)
            {
                fetchReservationsForClient()
            }
            else
            {
                fetchReservations()
            }
            setShouldFetch(false)
        }
    }, [shouldFetch])

    async function reset()
    {
        setCurrentPage(1)
        setReservations([])
        setShouldFetch(true)
    }

    async function fetchReservations()
    {
        try
        {
            const response = await mainBackendApi.get('/Reservation/GetAllReservations/' + usersPerPage + '/' + currentPage + '/' + onlyNewReservations + '/' + onlyUnrealizedReservations)
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
            const response = await mainBackendApi.get('/Reservation/GetClientReservations/' + usersPerPage + '/' + currentPage + '/' + onlyNewReservations + '/' + onlyUnrealizedReservations)
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
            <div>
                <label>TYLKO NOWE REZERWACJE</label>
                <input type={"checkbox"}
                       onChange={(e) =>
                       {
                           setOnlyNewReservations(e.target.checked)
                           reset()
                       }}
                       checked={onlyNewReservations}/>
            </div>
            <div>
                <label>TYLKO NIEZREALIZOWANE REZERWACJE</label>
                <input type={"checkbox"}
                       onChange={(e) =>
                       {
                           setOnlyUnrealizedReservations(e.target.checked)
                           reset()
                       }}
                       checked={onlyUnrealizedReservations}/>
            </div>
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Data rezerwacji</th>
                        <th>Początek rezerwacji</th>
                        <th>Koniec rezerwacji</th>
                        <th>Tor</th>
                        <th>Id klienta</th>
                        <th>Imię klienta</th>
                        <th>Nazwisko klienta</th>
                        <th>Faktura</th>
                    </tr>
                    </thead>
                    <tbody>
                    {reservations.map(reservation => (
                        <tr key={reservation.id}>
                            <td>{reservation.id}</td>
                            <td>{reservation.startTime.slice(0,10)}</td>
                            <td>{reservation.startTime.slice(11,16)}</td>
                            <td>{reservation.endTime.slice(11,16)}</td>
                            <td>{reservation.lane.laneNumber}</td>
                            <td>{reservation.client.user.id} </td>
                            <td>{reservation.client.person.firstName} </td>
                            <td>{reservation.client.person.lastName} </td>
                            <td>{reservation.invoice ? 'TAK' : (
                                (isClient === true) ?
                                    (
                                        'BRAK'
                                    )
                                    :
                                    (<button onClick={(e) =>
                                    {
                                        navigate('/management/sprzedaz/dodaj/'+reservation.id)
                                    }}>STWÓRZ FAKTURĘ</button>)
                            )}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <button onClick={(e) =>
            {
                isClient === true ? fetchReservationsForClient() : fetchReservations()
            }}>ZAŁADUJ DALEJ REZERWACJE
            </button>
            {(noMoreUsers === true) && <div className="no-more-users">ZAŁADOWANO JUŻ WSZYSTKIE</div>}
        </div>

    );
}