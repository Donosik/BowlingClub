import UserListTable from "./UserListTable";
import "./Users.css"
import lupa from "../Magazine/Vector.png";
import React, {useEffect, useState} from "react";
import { mainBackendApi} from "../../util/Requests";

export default function Users()
{
    const [users, setUsers] = useState([
        {
            Id: 1,
            Login: 'user1',
            IsClient: true,
            IsActive: true,
            Person: {
                FirstName: 'John',
                LastName: 'Doe',
                Email: 'john@example.com',
                DateOfBirth: '1990-01-01',
            },
        },
    ])

    useEffect(() =>
    {
        fetchUsers()
    },[])

    async function fetchUsers()
    {
        try
        {
            const response = await mainBackendApi().get('User/AllUsers')
            const data=response.data
            console.log(data)
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