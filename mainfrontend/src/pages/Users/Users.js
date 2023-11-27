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
            console.log(data)
        } catch (error)
        {
            console.log('Error fetching users:', error)
        }
    }

    function filteredUsers(users)
    {
        if(onlyWorker===true)
        {
            return users.filter((user)=>user.isClient===false)
        }
        return users
    }

    return (
        <>
            <div className="users-container">
                <div className="table-name">UŻYTKOWNICY</div>
                <input/>
                <img src={lupa} alt="lupa"/>
                Tylko pracownicy<input type="checkbox" onChange={()=>setOnlyWorker(!onlyWorker)}/>
                <button onClick={() => navigate('dodaj')}>DODAJ PRACOWNIKA
                </button>
                <br/>
                <UserListTable users={filteredUsers(users)}/></div>
        </>
    )
}