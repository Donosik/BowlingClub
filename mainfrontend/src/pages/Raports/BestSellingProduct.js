import React from "react";
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, registerables } from 'chart.js';
import * as htmlToImage from "html-to-image";
import { saveAs } from 'file-saver';

ChartJS.register(...registerables);

export default function BestSellingProduct({ sellingProducts }) {
    const chartData = {
        labels: sellingProducts.map((product) => product.ProductName),
        datasets: [
            {
                label: 'Ilość sprzedanych sztuk',
                backgroundColor: 'rgba(75,192,192,0.4)',
                borderColor: 'rgba(75,192,192,1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(75,192,192,0.6)',
                hoverBorderColor: 'rgba(75,192,192,1)',
                data: sellingProducts.map((product) => product.Sold),
            },
        ],
    };

    const chartOptions = {
        scales: {
            x: {
                title: {
                    display: true,
                    text: 'Nazwa produktu',
                },
            },
            y: {
                title: {
                    display: true,
                    text: 'Ilość sprzedanych sztuk',
                },
            },
        },
    };

    const chartId = 'best-selling-product-chart';

    const downloadChartAsPNG = () => {
        htmlToImage.toPng(document.getElementById(chartId))
            .then(function (dataUrl) {
                saveAs(dataUrl, 'best-selling-product-chart.png');
            });
    };

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
                <Bar id={chartId} data={chartData} options={chartOptions} />
                <button type="button" onClick={downloadChartAsPNG}>
                    Pobierz wykres jako PNG
                </button>
            </div>
        </div>
    );
}
