import "./UserListTable.css"
import {useNavigate} from "react-router-dom";
import {mainBackendApi} from "../../util/Requests";
export default function UserListTable({users},deletedUserCallback)
{
    const navigate=useNavigate()
    function formatData(data)
    {
        const dateObject=new Date(data)
        return dateObject.toLocaleDateString()
    }
    async function deleteUser(id)
    {
        const response = await mainBackendApi.delete('User/DeleteUser/'+id)
        console.log(response)
        vadeletedCallback=deletedUserCallback
    }
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
                <th>Czy admin</th>
                <th>Aktywny</th>
                <th>Email</th>
                <th>Data urodzenia</th>
                <th>Przyciski akcji</th>
            </tr>
            </thead>
            <tbody>
            {users.map((user) => (
                <tr key={user.id}>
                    <td>{user.id}</td>
                    <td>{user.person.firstName}</td>
                    <td>{user.person.lastName}</td>
                    <td>{user.login}</td>
                    <td>{user.isClient ? 'Yes' : 'No'}</td>
                    <td>{user.person.worker ? (user.person.worker.isAdmin?'Yes':'No'):'No' }</td>
                    <td>{user.isActive ? 'Yes' : 'No'}</td>
                    <td>{user.person.email}</td>
                    <td>{formatData(user.person.dateOfBirth)}</td>
                    <td><button onClick={()=>navigate('edytuj/'+user.id)}>EDYTUJ</button>
                        <button onClick={()=>deleteUser(user.id)}>USUŃ</button></td>
                </tr>
            ))}
            </tbody>
        </table></div>
    )
}