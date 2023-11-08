export default function MostWorkedHours({workersWithHours})
{
    return(
        <table>
            <thead>
            <tr>
                <th>ID</th>
                <th>Imię i nazwisko</th>
                <th>Email</th>
                <th>Liczba godzin</th>
            </tr>
            </thead>
            <tbody>
            {workersWithHours.map((worker)=>(
                <tr key={worker.Id}>
                    <td>{worker.Id}</td>
                    <td>{worker.FullName}</td>
                    <td>{worker.Email}</td>
                    <td>{worker.Hours}</td>
                </tr>
            ))}
            </tbody>
        </table>
    )
}