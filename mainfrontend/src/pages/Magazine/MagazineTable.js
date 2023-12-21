import "./Magazine_table.css"
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";

export default function MagazineTable({products})
{
    async function deleteProduct(id)
    {
        try
        {
            const response = await mainBackendApi.delete(`TargetInventory/${id}`)
        } catch (error)
        {
            console.log(error)
        }
    }

    return (
        <>

            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>Id Produktu</th>
                        <th>Nazwa produktu</th>
                        <th>Cena</th>
                        <th>Ilość docelowa</th>
                        <th>Ilość wolna</th>
                        <th>Przyciski akcji</th>
                    </tr>
                    </thead>
                    <tbody>
                    {products.map((product, index) => (
                        <tr key={index}>
                            <td>{product.id}</td>
                            <td>{product.name}</td>
                            <td>{product.price}</td>
                            <td>{product.targetQuantity}</td>
                            <td>{product.currentQuantity}</td>
                            <td>
                                <button>EDYTUJ</button>
                                <button onClick={e => deleteProduct(product.id)}>USUŃ</button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </>
    )
}