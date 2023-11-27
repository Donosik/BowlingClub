import React from 'react';

export default function WorkerSchedule({workersSchedule})
{

    return (
        <div className="table-container">
            <div className="table-name">GRAFIK PRACOWNIKÓW</div>
            <div className="table-container">
                <table className="table-bordered">
            <thead>
            <tr>
                <th>Id pracownika</th>
                <th>Imię pracownika</th>
                <th>Nazwisko pracownika</th>
                <th>Godziny pracy</th>
            </tr>
            </thead>
            <tbody>
            {workersSchedule.map(worker => (
                <tr key={worker.workerId}>
                    <td>{worker.Id}</td>
                    <td>{worker.FirstName}</td>
                    <td>{worker.LastName}</td>
                    <td>
                        <div className="table-container">
                        <table className="table-bordered">
                            <thead>
                            <tr>
                                <th>Godzina zaczęcia</th>
                                <th>Godzina skończenia</th>
                            </tr>
                            </thead>
                            <tbody>
                            {worker.Availability.map((slot, index) => (
                                <tr key={index}>
                                    <td>{slot.start}</td>
                                    <td>{slot.end}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table></div>
                    </td>

                </tr>
            ))}
            </tbody>
                </table></div></div>

    )
}