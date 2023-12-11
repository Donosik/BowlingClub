import {Bar} from "react-chartjs-2"
import {Chart as ChartJS, registerables} from 'chart.js'
import * as htmlToImage from "html-to-image"
import {saveAs} from 'file-saver'
import './Raports.css'

export default function MostWorkedHours({workersWithHours})
{
    ChartJS.register(...registerables)
    const chartData = {
        labels: workersWithHours.map(worker => worker.fullName),
        datasets: [
            {
                label: 'Total Work Hours',
                backgroundColor: 'rgba(75,192,192,0.4)',
                borderColor: 'rgba(75,192,192,1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(75,192,192,0.6)',
                hoverBorderColor: 'rgba(75,192,192,1)',
                data: workersWithHours.map(worker => worker.totalWorkHours),
            },
        ],
    };

    const chartOptions = {
        scales: {
            x: {
                title: {
                    display: true,
                    text: 'Full Name',
                },
            },
            y: {
                title: {
                    display: true,
                    text: 'Total Work Hours',
                },
            },
        },
    };

    const chartId = 'work-hours-chart'

    const downloadChartAsPNG = () =>
    {
        htmlToImage.toPng(document.getElementById(chartId))
            .then(function (dataUrl) {
                saveAs(dataUrl, 'my-node.png')
            });
    }
    return (
        <div className="table-container">
            <div className="table-name">RAPORTY</div>
            <br/>
            <br/>
            <div className="table-name">Najwięcej przepracowanych godzin</div>
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>ID</th>
                        <th>Imię i nazwisko</th>
                        <th>Email</th>
                        <th>Liczba godzin</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        workersWithHours.map((worker) => (
                            <tr key={worker.id}>
                                <td>{worker.id}</td>
                                <td>{worker.fullName}</td>
                                <td>{worker.email}</td>
                                <td>{worker.totalWorkHours}</td>
                            </tr>
                        ))
                    }
                    </tbody>
                </table>
            </div>
            <br/>
            <div className="chart-container">
                <Bar id={chartId} data={chartData} options={chartOptions} />
                <button type="button" onClick={downloadChartAsPNG}>Pobierz wykres jako PDF</button>
            </div>
        </div>
    )
}
