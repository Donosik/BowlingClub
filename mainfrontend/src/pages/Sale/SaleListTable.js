
export default function SaleListTable({sales})
{
    return(
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Data wystawienia</th>
                    <th>Data zapłaty</th>
                    <th>ID Klienta</th>
                    <th>Imię klienta</th>
                    <th>Nazwisko klienta</th>
                    <th>ID pracownika</th>
                    <th>Imię pracownika</th>
                    <th>Nazwisko pracownika</th>
                    <th>Zawartość</th>
                </tr>
            </thead>
            <tbody>
            {sales.map((sale)=>(
                <tr key={sale.Id}>
                    <td>{sale.Id}</td>
                    <td>{sale.IssueDate}</td>
                    <td>{sale.DueDate}</td>
                    <td>{sale.Client.Id}</td>
                    <td>{sale.Client.FirstName}</td>
                    <td>{sale.Client.LastName}</td>
                    <td>{sale.Worker.Id}</td>
                    <td>{sale.Worker.FirstName}</td>
                    <td>{sale.Worker.LastName}</td>
                    <td>{sale.content}</td>
                </tr>
            ))}
            </tbody>
        </table>
    )
}