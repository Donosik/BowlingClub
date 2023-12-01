import React from "react";
import Chart from "react-apexcharts";

export default function BestSellingProduct({ sellingProducts }) {
    // Przygotuj dane do wykresu
    const chartOptions = {
        chart: {
            id: "bar-chart",
            background: "#E6D1C0",
        },
        xaxis: {
            categories: sellingProducts.map((product) => product.ProductName),
        },
        plotOptions: {
            bar: {
                columnWidth: "50%", // Aby uzyskać efekt kolorowania słupków
                distributed: true,
            },
        },
        colors: ["#591914"], // Kolor słupków
    };

    const chartSeries = [
        {
            name: "Ilość sprzedanych sztuk",
            data: sellingProducts.map((product) => product.Sold),
        },
    ];

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

            {/* Dodaj wykres pod tabelą */}
            <div className="chart-container">
                <Chart
                    options={chartOptions}
                    series={chartSeries}
                    type="bar"
                    height={350}
                />
            </div>
        </div>
    );
}

