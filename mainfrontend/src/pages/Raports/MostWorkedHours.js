import React, {useEffect, useState} from "react";
import Chart from "react-apexcharts";
import {mainBackendApi} from "../../util/Requests";

export default function MostWorkedHours() {

    const [workersWithHours, setWorkersWithHours] = useState([])

    const [howManyDaysAgo, setHowManyDaysAgo] = useState(7)
    const [howManyDaysForward, setHowManyDaysForward] = useState(30)
    const [howManyTop, setHowManyTop] = useState(5)

    useEffect(() =>
    {
        fetchWorkersWithHours()
    }, [])


    async function fetchWorkersWithHours()
    {
        try
        {
            const response = await mainBackendApi.get('/Raport/MostWorkedHours/' + howManyDaysAgo + '/' + howManyDaysForward + '/' + howManyTop)
            const data = response.data
            setWorkersWithHours(data)
        } catch (error)
        {
            console.log(error)

        }
    }

    const chartOptions = {
        chart: {
            id: "bar-chart",
            background: "#E6D1C0",
        },
        xaxis: {
            categories: workersWithHours.map((worker) => worker.FullName),
            title: {
                text: 'IMIĘ I NAZWISKO', // Dodaj nazwę osi X
            },
        },
        yaxis: {
            title: {
                text: 'LICZBA GODZIN', // Dodaj nazwę osi Y
            },
        },
        plotOptions: {
            bar: {
                colors: {
                    ranges: [
                        {
                            from: 0,
                            to: Math.max(...workersWithHours.map((worker) => worker.totalWorkHours)),
                            color: "#591914", // Zmień kolor słupków na #4CAF50
                        },
                    ],
                },
            },
        },
        title: {
            text: 'NAJWIĘCEJ PRZEPRACOWANYCH GODZIN', // Dodaj tytuł wykresu
            align: 'center',
            style: {
                fontSize: '18px',
            },
        },
    };

    const chartSeries = [
        {
            name: "Liczba godzin",
            data: workersWithHours.map((worker) => worker.totalWorkHours),
        },
    ];

    return (
        <div className="table-container">
            <div className="table-name">RAPORTY</div>
            <br />
            <br />
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
                    {workersWithHours.map((worker) => (
                        <tr key={worker.Id}>
                            <td>{worker.Id}</td>
                            <td>{worker.FullName}</td>
                            <td>{worker.Email}</td>
                            <td>{worker.totalWorkHours}</td>
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
