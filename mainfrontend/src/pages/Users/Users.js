import UserListTable from "./UserListTable";
import "./Users.css"
import lupa from "../Magazine/Vector.png";
import React from "react";
export default function Users()
{
    const users = [
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
    ]

    return (
        <>
            <div className="users-container">
                <div className="table-name">UŻYTKOWNICY</div>
                <input/><img src={lupa} alt="lupa"/><button>DODAJ UŻYTKOWNIKA
            </button><br/>

                <UserListTable users={users}/> </div>
        </>
    )
}