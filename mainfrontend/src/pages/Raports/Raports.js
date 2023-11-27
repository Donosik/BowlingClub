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

            <div className="table-container">
                <form>
            <MostWorkedHours workersWithHours={workersWithHours}/>
                </form></div><br/>
        
            <div className="table-container">
                <form><BestBuyingClient buyingClients={buyingClient}/> </form></div>

            <br/><div className="table-container">
                <form><BestSellingProduct sellingProducts={sellingProducts}/> </form></div>
        </>
    )
}