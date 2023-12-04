import React, {useEffect, useState} from 'react'
import axios from 'axios'
import {mainBackendApi} from '../../util/Requests'
import ChangeRegulations from "./ChangeRegulations";

export default function ChangeOpenHours()
{
    const [openHours, setOpenHours] = useState([])
    const [editedOpenHours, setEditedOpenHours] = useState([])

    useEffect(() =>
    {
        fetchOpeningHours()
    }, [])

    function getDayName(dayNumber)
    {
        const daysOfWeek = ['NIEDZIELA', 'PONIEDZIAŁEK', 'WTOREK', 'ŚRODA', 'CZWARTEK', 'PIĄTEK', 'SOBOTA'];
        return daysOfWeek[dayNumber];
    }

    async function fetchOpeningHours()
    {
        try
        {
            const response = await mainBackendApi.get('Data')
            const data = response.data
            setOpenHours(data)
            setEditedOpenHours(data)
        } catch (error)
        {
            console.error('Error fetching opening hours:', error)
        }
    }

    function getSmallHour(hour)
    {
        const date = new Date(`2000-01-01T${hour}`);
        return date.toLocaleTimeString('pl-PL', {hour: '2-digit', minute: '2-digit'});
    }

    function getBigHour(hour)
    {
        const date = new Date(`2000-01-01T${hour}`);
        return date.toLocaleTimeString('pl-PL', {hour: '2-digit', minute: '2-digit', second: '2-digit'});
    }

    function handleInputChange(index, field, value)
    {
        // Update the edited opening hours state based on user input
        const updatedOpenHours = [...editedOpenHours]
        updatedOpenHours[index] = {...updatedOpenHours[index], [field]: getBigHour(value)}
        setEditedOpenHours(updatedOpenHours)
    }

    async function handleSaveChanges()
    {
        try
        {
            const formattedOpenHours = editedOpenHours.map((day) => ({
                ...day,
                startTime: getBigHour(day.startTime),
                endTime: getBigHour(day.endTime),
            }))
            const response = await mainBackendApi.put('Data', formattedOpenHours)
        } catch (error)
        {
            console.error('Error updating opening hours:', error)
        }
        await fetchOpeningHours()
    }

    async function createOpenHours()
    {
        const response = await mainBackendApi.post('Data')
        console.log(response)
        await fetchOpeningHours()
    }

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZMIANA GODZIN OTWARCIA LOKALU</div>
                    <button type="button"
                            onClick={(e) => createOpenHours()}>Stwórz domyślny harmonogram
                    </button>
                    <div className="table-container">
                        <table className="table-bordered">
                            <thead>
                            <tr>
                                <th>Dzień</th>
                                <th>Godzina otwarcia</th>
                                <th>Godzina zamknięcia</th>
                            </tr>
                            </thead>
                            <tbody>
                            {editedOpenHours.map((day, index) => (
                                <tr key={index}>
                                    <td>{getDayName(day.dayOfWeek)}</td>
                                    <td>
                                        <input
                                            type="time"
                                            value={getSmallHour(day.startTime)}
                                            onChange={(e) =>
                                                handleInputChange(index, 'startTime', e.target.value)
                                            }
                                        />
                                    </td>
                                    <td>
                                        <input
                                            type="time"
                                            value={getSmallHour(day.endTime)}
                                            onChange={(e) =>
                                                handleInputChange(index, 'endTime', e.target.value)
                                            }
                                        />
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                    <div className="d-flex">
                        <button type="button"
                                onClick={handleSaveChanges}>
                            ZAPISZ ZMIANY
                        </button>
                    </div>
                </form>
            </div>
        </>
    )
}