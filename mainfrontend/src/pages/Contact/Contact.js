import React, { useState, useEffect } from 'react';
import './Contact.css';
import { mainBackendApi } from '../../util/Requests';

export default function Contact() {
    const [openingHours, setOpeningHours] = useState([]);

    useEffect(() => {
        fetchOpeningHours();
    }, []);

    async function fetchOpeningHours() {
        try {
            const response = await mainBackendApi.get('Data');
            const data = response.data;
            setOpeningHours(data);
        } catch (error) {
            console.error('Error fetching opening hours:', error);
        }
    }

    return (
        <>
            <div className="contact-wrapper">
                <div className="contact-info">
                    <h2>KONTAKT</h2>
                    <p>Adres: ul. Przykładowa 123, 00-000 Miasto</p>
                    <p>Telefon: 123-456-789</p>
                    <p>Email: kontakt@kregielnia.pl</p>
                </div>
                <div className="opening-hours">
                    <h2>GODZINY OTWARCIA</h2>
                    <ul>
                        {openingHours.map((day, index) => (
                            <li key={index}>
                                {getDayName(day.dayOfWeek)}: {getSmallHour(day.startTime)} - {getSmallHour(day.endTime)}
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </>
    );
}

function getDayName(dayNumber) {
    const daysOfWeek = ['NIEDZIELA', 'PONIEDZIAŁEK', 'WTOREK', 'ŚRODA', 'CZWARTEK', 'PIĄTEK', 'SOBOTA'];
    return daysOfWeek[dayNumber];
}

function getSmallHour(hour) {
    return hour.slice(0, -3);
}
