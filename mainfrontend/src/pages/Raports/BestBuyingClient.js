import React from "react";
import {Bar} from "react-chartjs-2";
import {Chart as ChartJS, registerables} from 'chart.js';
import * as htmlToImage from "html-to-image";
import {saveAs} from 'file-saver';

ChartJS.register(...registerables);

export default function BestBuyingClient({buyingClients})
{
    const chartData = {
        labels: buyingClients.map((client) => client.fullName),
        datasets: [
            {
                label: 'NAJLEPSI KLIENCI',
                backgroundColor: 'rgba(89,25,20,1)',
                borderColor: 'rgba(230,209,192,1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(89,25,20,0.8)',
                hoverBorderColor: 'rgba(89,25,20,0.5)',
                data: buyingClients.map((client) => client.totalMoneySpend),
            },
        ],
    };

    const chartOptions = {
        scales: {
            x: {
                title: {
                    display: true,
                    text: 'IMIĘ I NAZWISKO',
                },
            },
            y: {
                title: {
                    display: true,
                    text: 'LICZBA FAKTUR',
                },
            },
        },
    };

    const chartId = 'best-buying-client-chart';

    const downloadChartAsPNG = () =>
    {
        htmlToImage.toPng(document.getElementById(chartId))
            .then(function (dataUrl)
            {
                saveAs(dataUrl, 'best-buying-client-chart.png');
            });
    };

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
                    {buyingClients.map((client,index) => (
                        <tr key={index}>
                            <td>{client.id}</td>
                            <td>{client.fullName}</td>
                            <td>{client.email}</td>
                            <td>{client.totalMoneySpend}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <br/>
            <div className="login-box">
                <Bar id={chartId}
                     data={chartData}
                     options={chartOptions}/>
                <button type="button"
                        onClick={downloadChartAsPNG}>
                    Pobierz wykres jako PNG
                </button>
            </div>
        </div>
    );
}
