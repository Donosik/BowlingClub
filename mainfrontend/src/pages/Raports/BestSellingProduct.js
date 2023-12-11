import React from "react";
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, registerables } from 'chart.js';
import * as htmlToImage from "html-to-image";
import { saveAs } from 'file-saver';

ChartJS.register(...registerables);

export default function BestSellingProduct({ sellingProducts }) {
    const chartData = {
        labels: sellingProducts.map((product) => product.productName),
        datasets: [
            {
                label: 'NAJLEPIEJ SPRZEDAJĄCE SIĘ PRODUKTY',
                backgroundColor: 'rgba(89,25,20,1)',
                borderColor: 'rgba(230,209,192,1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(89,25,20,0.8)',
                hoverBorderColor: 'rgba(89,25,20,0.5)',
                data: sellingProducts.map((product) => product.totalSold),
            },
        ],
    };

    const chartOptions = {
        scales: {
            x: {
                title: {
                    display: true,
                    text: 'NAZWA PRODUKTU',
                },
            },
            y: {
                title: {
                    display: true,
                    text: 'ILOŚĆ SZTUK SPRZEDANYCH',
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
                    {sellingProducts.map((product,index) => (
                        <tr key={index}>
                            <td>{product.id}</td>
                            <td>{product.productName}</td>
                            <td>{product.totalSold}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <br />
            <div className="login-box">
                <Bar id={chartId} data={chartData} options={chartOptions} />
                <button type="button" onClick={downloadChartAsPNG}>
                    Pobierz wykres jako PNG
                </button>
            </div>
        </div>
    );
}
