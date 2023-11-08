import SaleListTable from "./SaleListTable";

export default function Sale()
{
    const sales = [
        {
            Id: 1,
            IssueDate: '1990-01-01',
            DueDate: '1990-01-01',
            Client:
                {
                    Id: 1,
                    FirstName: 'John',
                    LastName: 'Doe',
                },
            Worker:
                {
                    Id: 1,
                    FirstName: 'John',
                    LastName: 'Doe',
                },
            content: 'zawartosc'
        },
    ]

    return (
        <>
            Sprzedaż - lista faktur
            <SaleListTable sales={sales}/>
        </>
    )
}