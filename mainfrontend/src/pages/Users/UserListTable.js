import "./UserListTable.css"
export default function UserListTable({users})
{
    return(
        <div className="table-container">
        <table className="table-bordered">
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
                <th>Przyciski akcji</th>
            </tr>
            </thead>
            <tbody>

            </tbody>
        </table></div>
    )
}