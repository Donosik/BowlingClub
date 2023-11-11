import React, {useEffect, useState} from 'react'
import axios from 'axios'
import {fetchAdress} from '../../util/Requests'

export default function ChangeSite()
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
            const response = await axios.get(`${fetchAdress()}Data`)
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

            const response = await axios.put(
                fetchAdress() + 'Data',
                formattedOpenHours
            )
        } catch (error)
        {
            console.error('Error updating opening hours:', error)
        }
        await fetchOpeningHours()
    }

    return (
        <>
            <div>
                <h2>Change Opening Hours</h2>
                <form>
                    <table>
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
                    <button type="button"
                            onClick={handleSaveChanges}>
                        Save Changes
                    </button>
                </form>
            </div>
        </>
    )
}