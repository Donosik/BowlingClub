export default function BestSellingProduct({sellingProducts})
{
    return(
        <table>
            <thead>
            <tr>
                <th>ID</th>
                <th>Nazwa produktu</th>
                <th>Ilość sprzedanych sztuk</th>
            </tr>
            </thead>
            <tbody>
            {sellingProducts.map((product)=>(
                <tr key={product.Id}>
                    <td>{product.Id}</td>
                    <td>{product.ProductName}</td>
                    <td>{product.Sold}</td>
                </tr>
            ))}
            </tbody>
        </table>
    )
}