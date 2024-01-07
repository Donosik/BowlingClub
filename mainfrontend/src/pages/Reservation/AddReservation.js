import Calendar from "react-calendar";
import React, {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import {getIsWorker} from "../../util/UserType";
import './WorkerSchedule.css'

export default function AddReservation()
{
    const [calendarVisible, setCalendarVisible] = useState(true)
    const [isAddingFailed, setIsAddingFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("")
    const navigate = useNavigate()
    const [selectedDate, setSelectedDate] = useState(new Date())
    const [startTime, setStartTime] = useState("")
    const [endTime, setEndTime] = useState("")
    const [openingTime, setOpeningTime] = useState("");
    const [closingTime, setClosingTime] = useState("");

    useEffect(() => {
        const getOpeningHours = async () => {
            const openingHours = await fetchOpeningHours()
            const currentDay = new Date().getDay()
            const currentDayHours = openingHours.find((hours) => hours.dayOfWeek === currentDay)

            if (currentDayHours) {
                setOpeningTime(currentDayHours.startTime.substring(0, 5))
                setClosingTime(currentDayHours.endTime.substring(0, 5))
                console.log(currentDayHours.startTime.substring(0, 5))
                console.log(currentDayHours.endTime.substring(0, 5))
            }
        }
        getOpeningHours()
    }, [])

    useEffect(() => {
        validateTimes()
    }, [startTime, endTime])

    const validateTimes = () => {
        if (startTime && endTime) {
            if (startTime >= endTime) {
                setEndTime("")
            }
        }
    }

    async function fetchOpeningHours() {
        try {
            const response = await mainBackendApi.get('Data')
            return response.data

        } catch (error) {
            console.error('Error fetching opening hours:', error)
        }
    }

    async function handleSubmit()
    {
        try
        {
            setIsAddingFailed(false)
            const startDate = new Date(selectedDate);
            const startTimeDate = new Date(`1970-01-01T${startTime}`);
            const endTimeDate = new Date(`1970-01-01T${endTime}`);

            startDate.setHours(startTimeDate.getHours(), startTimeDate.getMinutes());
            const endDate = new Date(selectedDate);
            endDate.setHours(endTimeDate.getHours(), endTimeDate.getMinutes());

            const request = {
                startTime: startDate.toISOString(),
                endTime: endDate.toISOString(),
            };
            const response=await mainBackendApi.post('Reservation/MakeReservation',request)

            if (getIsWorker() === true)
                navigate('/management/rezerwacje')
            else
                navigate('/rezerwacje')
        } catch (e)
        {
            console.log(e)
            setIsAddingFailed(true)
            setErrorMessage("BŁĄD")
        }
    }

    return (
        <>
            <div className="auth-page">
                <div className="container page d-flex justify-content-center align-items-center">
                    <div className="row">
                        <div className="form-box">
                            <h1 className="text-login">DODAWANIE REZERWACJI</h1>

                            <div className="calendar-container">
                                <button type="button"
                                        onClick={() => setCalendarVisible(!calendarVisible)}>
                                    {calendarVisible ? 'UKRYJ KALENDARZ REZERWACJI' : 'POKAŻ KALENDARZ REZERWACJI'}
                                </button>
                                {calendarVisible && (
                                    <>
                                        <Calendar
                                            value={selectedDate || new Date()}
                                            onChange={(date) => setSelectedDate(date)}
                                        />
                                        <br/>
                                    </>
                                )}
                            </div>
                            <div>
                                <label htmlFor="laneNumber">GODZINA ROZPOCZĘCIA</label>
                                <input
                                    type="time"
                                    id="startTime"
                                    min={openingTime}
                                    max={closingTime}
                                    onChange={e => setStartTime(e.target.value)}
                                    value={startTime}
                                />
                            </div>
                            <div>
                                <label htmlFor="laneNumber">GODZINA ZAKOŃCZENIA</label>
                                <input
                                    type="time"
                                    id="startTime"
                                    min={openingTime}
                                    max={closingTime}
                                    onChange={e => setEndTime(e.target.value)}
                                    value={endTime}
                                />
                            </div>
                            <div className="d-flex justify-content-center align-items-center">
                                <button type="button"
                                        onClick={handleSubmit}>STWÓRZ REZERWACJĘ
                                </button>
                            </div>
                            {(isAddingFailed === true) && errorMessage}

                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}