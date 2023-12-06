import React, { useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import './WorkerSchedule.css';

export default function WorkerSchedule({ workersSchedule }) {
    const [selectedWorker, setSelectedWorker] = useState(null);
    const [selectedDate, setSelectedDate] = useState(null);
    const [calendarVisible, setCalendarVisible] = useState(false);

    const handleWorkerClick = (worker) => {
        setSelectedWorker(worker);
        setCalendarVisible(true);
    };

    const handleCloseCalendar = () => {
        setCalendarVisible(false);
    };

    const handleCalendarChange = (date) => {
        setSelectedDate(date);
    };

    const getWorkersForSelectedDate = () => {
        if (!selectedDate) return [];

        return workersSchedule.filter((worker) =>
            worker.Availability.some(
                (slot) =>
                    new Date(slot.start) && new Date(slot.start).toLocaleDateString() ===
                    selectedDate.toLocaleDateString()
            )
        );
    };

    const workersForSelectedDate = getWorkersForSelectedDate();

    return (
        <React.Fragment>
        <div className="table-name">GRAFIK PRACOWNIKÓW</div>
        <div className="d-flex gap-3">
            <div>
                <div className="calendar-container">

                    {calendarVisible && (
                        <>
                            <Calendar
                                value={selectedDate || new Date()}
                                onChange={handleCalendarChange}
                            />
                            <br />
                        </>
                    )}
                </div>
                <button type="button" onClick={() => setCalendarVisible(!calendarVisible)}>
                    {calendarVisible ? 'UKRYJ KALENDARZ' : 'POKAŻ KALENDARZ'}
                </button>
                <button type="button">DODAJ ZMIANĘ</button>
            </div>
            {selectedDate && (
                <div>
                    <div className="table-name">
                        PRACOWNICY W DNIU {selectedDate.toLocaleDateString()}
                    </div>
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
                            {workersForSelectedDate.map((worker) => (
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
                    </div>
                </div>
            )}
        </div>
        </React.Fragment>
    );
}