import UserListTable from "./UserListTable";
import "./Users.css"
import lupa from "../Magazine/Vector.png";
import React, {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function Users()
{
    const [users, setUsers] = useState([])
    const [onlyWorker, setOnlyWorker] = useState(false)
    const [filter, setFilter] = useState("")
    const navigate = useNavigate();

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

    function filteredUsers(users)
    {   const filtered=users
        if (onlyWorker === true)
        {
            return filtered.filter((user) => user.isClient === false)
        }
        if (filter !== "")
        {
            return filtered.filter((user) => user.person.lastName.toLowerCase().includes(filter.toLowerCase()))
        }
        return filtered
    }

    return (
        <>
            <div className="users-container">
                <div className="table-name">UŻYTKOWNICY</div>
                <input type={"text"} onChange={e=>setFilter(e.target.value)}/>
                <img src={lupa}
                     alt="lupa"/>
                <button onClick={() => navigate('dodaj')}>DODAJ PRACOWNIKA
                </button>
                <br/>
                <input type="checkbox"
                       onChange={() => setOnlyWorker(!onlyWorker)}/>
                POKAŻ TYLKO PRACOWNIKÓW<br/>

                <br/>
                <UserListTable users={filteredUsers(users)}
                               deletedUserCallback={fetchUsers}/></div>
        </>
    )
}