import React, { useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import './WorkerSchedule.css';

export default function WorkerSchedule({ workersSchedule }) {
    const [selectedWorker, setSelectedWorker] = useState(null);
    const [selectedDate, setSelectedDate] = useState(null);

    const handleWorkerClick = (worker) => {
        setSelectedWorker(worker);
    };

    const handleCloseCalendar = () => {
        setSelectedWorker(null);
    };

    const handleCalendarChange = (date) => {
        setSelectedDate(date);
    };

    const getWorkersForSelectedDate = () => {
        if (!selectedDate) return [];

        return workersSchedule.filter((worker) =>
            worker.Availability.some(
                (slot) =>
                    new Date(slot.start).toLocaleDateString() ===
                    selectedDate.toLocaleDateString()
            )
        );
    };

    return (
        <div className="table-container">
            <div className="table-name">GRAFIK PRACOWNIKÓW</div>



            <div className="calendar-container">

                <Calendar
                    value={selectedDate || new Date()}
                    onChange={handleCalendarChange}

                />
                {selectedDate && (
                    <div>
                        <div className="table-name">PRACOWNICY W DNIU {selectedDate.toLocaleDateString()}</div>
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
                            {getWorkersForSelectedDate().map((worker) => (
                                <tr key={worker.workerId}>
                                    <td>{worker.Id}</td>
                                    <td>{worker.FirstName}</td>
                                    <td>{worker.LastName}</td>
                                    <td>
                                        {worker.Availability
                                            .filter(
                                                (slot) =>
                                                    new Date(slot.start).toLocaleDateString() ===
                                                    selectedDate.toLocaleDateString()
                                            )
                                            .map((slot, index) => (
                                                <div key={index}>
                                                    {slot.start} - {slot.end}
                                                </div>
                                            ))}
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div> </div>
                )}
                <button onClick={handleCloseCalendar}>Zamknij kalendarz</button>
            </div>
        </div>
    );
}
