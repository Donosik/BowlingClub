import React from "react";

export default function BestSellingProduct({ sellingProducts }) {

    return (
        <div className="table-container">
            <div className="table-name">Najlepiej sprzedające się produkty</div>
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nazwa produktu</th>
                        <th>Ilość sprzedanych sztuk</th>
                    </tr>
                    </thead>
                    <tbody>
                    {sellingProducts.map((product) => (
                        <tr key={product.Id}>
                            <td>{product.Id}</td>
                            <td>{product.ProductName}</td>
                            <td>{product.Sold}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <br />

            <div className="chart-container">

            </div>
        </div>
    );
}
