import React from "react";
import Chart from "react-apexcharts";

export default function BestBuyingClient({ buyingClients }) {
    // Przygotuj dane do wykresu
// Przygotuj dane do wykresu
    const chartOptions = {
        chart: {
            id: "bar-chart",
            background: "#E6D1C0",
        },
        xaxis: {
            categories: buyingClients.map((client) => client.FullName),
            title: {
                text: 'KLIENCI', // Dodaj nazwę osi X
            },
        },
        yaxis: {
            title: {
                text: 'ZYSK ZE SPRZEDAŻY', // Dodaj nazwę osi Y
            },
        },
        plotOptions: {
            bar: {
                colors: {
                    ranges: [
                        {
                            from: 0,
                            to: 200,
                            color: "#591914", // Zmień kolor słupków na #4CAF50
                        },
                    ],
                },
            },
        },
        title: {
            text: 'NAJLEPSI KLIENCI', // Dodaj tytuł wykresu
            align: 'center',
            style: {
                fontSize: '18px',
            },
        },
    };

    const chartSeries = [
        {
            name: "Liczba faktur",
            data: buyingClients.map((client) => client.Invoices),
        },
    ];

    return (
        <div className="table-container">
            <div className="table-name">Najlepsi klienci</div>
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Imię i nazwisko</th>
                        <th>Email</th>
                        <th>Liczba faktur</th>
                    </tr>
                    </thead>
                    <tbody>
                    {buyingClients.map((client) => (
                        <tr key={client.Id}>
                            <td>{client.Id}</td>
                            <td>{client.FullName}</td>
                            <td>{client.Email}</td>
                            <td>{client.Invoices}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
<br/>
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
