import ReservationTable from "./ReservationTable";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../../util/UserType";
import {mainBackendApi} from "../../util/Requests";

export default function Reservation()
{
    const [isClient, setIsClient] = React.useState(false)
    const navigate = useNavigate()

    useEffect(() =>
    {
        if (isUserLoggedIn() === true && getIsWorker() === false && getIsAdmin() === false)
        {
            setIsClient(true)
        }
        else
        {
            setIsClient(false)
        }
    }, []);

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZARZĄDZANIE REZERWACJAMI</div>

                    {(isClient === true) && <button onClick={() => navigate('dodaj')}>DODAJ REZERWACJE</button>}
                    <ReservationTable/>
                </form>
            </div>
        </>
    )
}