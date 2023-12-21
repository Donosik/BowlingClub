import './home.css';


export default function Home()
{
    return (
        <>
            
            <div className="magazine-container">
            <div className="table-container">
            <table className="table-bordered">
                <thead>
                <tr>
                    <th>PRODUKT</th>
                    <th>ILOŚĆ</th>
                    <th>AKCJA</th>
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td>Produkt </td>
                    <td>10</td>
                    <td><button>Akcja 1</button></td>
                </tr>
                </tbody>
            </table> </div></div>
        </>
    )
}