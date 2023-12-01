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
            title: {
                text: 'NAZWA PRODUKTU', // Dodaj nazwę osi X
            },
        },
        yaxis: {
            title: {
                text: 'ILOŚĆ SPRZEDANYCH SZTUK', // Dodaj nazwę osi Y
            },
        },
        plotOptions: {
            bar: {
                colors: {
                    ranges: [
                        {
                            from: 0,
                            to: Math.max(...sellingProducts.map((product) => product.Sold)),
                            color: "#591914", // Zmień kolor słupków na #4CAF50
                        },
                    ],
                },
            },
        },
        title: {
            text: 'NAJLEPIEJ SPRZEDAJĄCE SIĘ PRODUKTY', // Dodaj tytuł wykresu
            align: 'center',
            style: {
                fontSize: '18px',
            },
        },
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
            <br />
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
