import MostWorkedHours from "./MostWorkedHours";
import BestBuyingClient from "./BestBuyingClient";
import BestSellingProduct from "./BestSellingProduct";

export default function Raports()
{
    const workersWithHours = [{
        Id: 1,
        FullName: 'John Doe',
        Email: 'john@example.com',
        Hours: 80,
    },]

    const buyingClient = [{
        Id: 1,
        FullName: 'John Doe',
        Email: 'john@example.com',
        Invoices: 10,
    },]

    const sellingProducts=[{
        Id:1,
        ProductName:'Drink',
        Sold:1500,
    },]

    return (
        <>
            Raporty
            Najwiecej przepracowanych godzin
            <MostWorkedHours workersWithHours={workersWithHours}/>
            Najlepsi klienci
            <BestBuyingClient buyingClients={buyingClient}/>
            Najlepiej sprzedające się produkty
            <BestSellingProduct sellingProducts={sellingProducts}/>
        </>
    )
}