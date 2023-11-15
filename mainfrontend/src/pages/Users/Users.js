import UserListTable from "./UserListTable";
import "./Users.css"
import lupa from "../Magazine/Vector.png";
import React, {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function Users()
{
    const [users, setUsers] = useState([])

    useEffect(() =>
    {
        fetchUsers()
    }, [])

    async function fetchUsers()
    {
        try
        {
            const response = await mainBackendApi.get('User/AllUsers')
            const data = response.data
            setUsers(data)
        } catch (error)
        {
            console.log('Error fetching users:', error)
        }
    }

    return (
        <>
            <div className="users-container">
                <div className="table-name">UŻYTKOWNICY</div>
                <input/><img src={lupa}
                             alt="lupa"/>
                <button>DODAJ UŻYTKOWNIKA
                </button>
                <br/>
                <UserListTable users={users}/></div>
        </>
    )
}