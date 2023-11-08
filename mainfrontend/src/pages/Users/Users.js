import UserListTable from "./UserListTable";

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
            Uzytkownicy
            <UserListTable users={users}/>
        </>
    )
}