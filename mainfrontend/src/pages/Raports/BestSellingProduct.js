import React from "react"
import {Bar} from "react-chartjs-2"
import {Chart as ChartJS, registerables} from 'chart.js'
import * as htmlToImage from "html-to-image"
import {saveAs} from 'file-saver'
import './Raports.css'

ChartJS.register(...registerables)

export default function BestSellingProduct({sellingProducts})
{
    const chartData = {
        labels: sellingProducts.map((product) => product.productName),
        datasets: [
            {
                label: 'Ilość sprzedanych sztuk',
                backgroundColor: 'rgba(89,25,20,0.4)',
                borderColor: 'rgba(89,25,20,1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(89,25,20,0.6)',
                hoverBorderColor: 'rgba(89,25,20,1)',
                data: sellingProducts.map((product) => product.totalSold),
            },
        ],
    }

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
    }

    const chartId = 'best-selling-product-chart'

    const downloadChartAsPNG = () =>
    {
        htmlToImage.toPng(document.getElementById(chartId))
            .then(function (dataUrl)
            {
                saveAs(dataUrl, 'best-selling-product-chart.png')
            })
    }

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
                    {sellingProducts.map((product, index) => (
                        <tr key={index}>
                            <td>{product.id}</td>
                            <td>{product.productName}</td>
                            <td>{product.totalSold}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <br/>
            <div>
                <div id={chartId}
                     className="chart-container">
                    <Bar data={chartData}
                         options={chartOptions}/>
                </div>
                <button type="button"
                        onClick={downloadChartAsPNG}>
                    Pobierz wykres jako PNG
                </button>
            </div>
        </div>
    )
}
