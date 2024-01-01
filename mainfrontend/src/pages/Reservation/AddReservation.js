import Calendar from "react-calendar";
import React, {useState} from "react";
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import {getIsWorker} from "../../util/UserType";

export default function AddReservation()
{
    const [calendarVisible, setCalendarVisible] = useState(true)
    const [isAddingFailed, setIsAddingFailed] = useState(false)
    const [errorMessage, setErrorMessage] = useState("")
    const navigate = useNavigate()
    const [selectedDate, setSelectedDate] = useState(new Date())
    const [startTime, setStartTime] = useState("")
    const [endTime, setEndTime] = useState("")

    async function handleSubmit()
    {
        try
        {
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
            console.log(request)

            if (getIsWorker() === true)
                navigate('/management/rezerwacje')
            else
                navigate('/rezerwacje')
        } catch (e)
        {
            console.log(e)
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
                                <input type="time" onChange={e=>setStartTime(e.target.value)}/>
                            </div>
                            <div>
                                <label htmlFor="laneNumber">GODZINA ZAKOŃCZENIA</label>
                                <input type="time" onChange={e=>setEndTime(e.target.value)}/>
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