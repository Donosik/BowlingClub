export default function MagazineTable({products})
{
    return(
        <>
            <h2>Bowling Alley Inventory</h2>
            <table>
                <thead>
                <tr>
                    <th>Id Produktu</th>
                    <th>Nazwa produktu</th>
                </tr>
                </thead>
                <tbody>
                {products.map((product, index) => (
                    <tr key={index}>
                        <td>{product.Id}</td>
                        <td>{product.Name}</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </>
    )
}