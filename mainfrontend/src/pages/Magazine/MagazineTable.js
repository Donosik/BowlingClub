import "./Magazine_table.css"
export default function MagazineTable({products})
{
    return(
        <>
            <div className="table-name">MAGAZYN PRZEDMIOTÓW</div>
            <div className="table-container">
            <table className="table-bordered">
                <thead>
                <tr>
                    <th>Id Produktu</th>
                    <th>Nazwa produktu</th>
                    <th>Przyciski akcji</th>
                </tr>
                </thead>
                <tbody>
                {products.map((product, index) => (
                    <tr key={index}>
                        <td>{product.Id}</td>
                        <td>{product.Name}</td>
                        <td><button>USUŃ</button>
                            <button>EDYTUJ</button></td>
                    </tr>
                ))}
                </tbody>
            </table>
            </div>
        </>
    )
}