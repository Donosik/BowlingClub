export default function UserListTable({users})
{
    return(
        <table>
            <thead>
            <tr>
                <th>ID</th>
                <th>Imię</th>
                <th>Nazwisko</th>
                <th>Login</th>
                <th>Czy klient</th>
                <th>Aktywny</th>
                <th>Email</th>
                <th>Data urodzenia</th>
            </tr>
            </thead>
            <tbody>
            {users.map((user) => (
                <tr key={user.Id}>
                    <td>{user.Id}</td>
                    <td>{user.Person.FirstName}</td>
                    <td>{user.Person.LastName}</td>
                    <td>{user.Login}</td>
                    <td>{user.IsClient ? 'Yes' : 'No'}</td>
                    <td>{user.IsActive ? 'Yes' : 'No'}</td>
                    <td>{user.Person.Email}</td>
                    <td>{user.Person.DateOfBirth}</td>
                </tr>
            ))}
            </tbody>
        </table>
    )
}