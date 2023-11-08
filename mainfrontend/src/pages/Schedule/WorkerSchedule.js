import React from 'react';

export default function WorkerSchedule({workersSchedule})
{

    return (
        <table>
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
                        <table>
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
                        </table>
                    </td>

                </tr>
            ))}
            </tbody>
        </table>

    )
}