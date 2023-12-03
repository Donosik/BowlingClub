import './home.css'
import bowling_photo from './pobrany plik.jpeg'
import {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function Home()
{
    // Default data hours in case there is no backend connection
    const [openingHours, setOpeningHours] = useState([
        {
            "id": 0,
            "dayOfWeek": 1,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 1,
            "dayOfWeek": 2,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 2,
            "dayOfWeek": 3,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 3,
            "dayOfWeek": 4,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 4,
            "dayOfWeek": 5,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 5,
            "dayOfWeek": 6,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        },
        {
            "id": 6,
            "dayOfWeek": 0,
            "startTime": "10:00:00",
            "endTime": "20:00:00"
        }
    ])
    const [regulations, setRegulations] = useState([])

    useEffect(() =>
    {
        fetchOpeningHours()
        fetchRegulations()
    }, [])

    async function fetchOpeningHours()
    {
        try
        {
            const response = await mainBackendApi.get('Data')
            const data = response.data
            setOpeningHours(data)
        } catch (error)
        {
            console.error('Error fetching opening hours:', error)
        }
    }

    async function fetchRegulations()
    {
        try
        {
            const response = await mainBackendApi.get("Regulation");
            const data = response.data;
            setRegulations(data);
        } catch (error)
        {
            console.error("Error fetching regulations:", error);
        }
    }


    function getDayName(dayNumber)
    {
        const daysOfWeek = ['NIEDZIELA', 'PONIEDZIAŁEK', 'WTOREK', 'ŚRODA', 'CZWARTEK', 'PIĄTEK', 'SOBOTA'];
        return daysOfWeek[dayNumber];
    }

    function getSmallHour(hour)
    {
        return hour.slice(0, -3)
    }

    return (
        <>
            <div className="container d-flex w-100 justify-content-center align-content-center content-container">
                <div className="d-flex align-items-center flex-row flex-wrap time-baba justify-content-between">
                    <div className="box-1 flex-grow-1">
                        <p>GODZINY OTWARCIA</p>
                        <div className="date-time-container d-flex flex-column justify-content-between">
                            {openingHours.map((day, index) => (
                                <div className="date-time d-flex flex-row justify-content-between"
                                     key={index}>
                                    <span>{getDayName(day.dayOfWeek)}</span>
                                    <span>{getSmallHour(day.startTime)} - {getSmallHour(day.endTime)}</span>
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="box-1 flex-grow-1">
                        <p>NASZE PROMOCJE</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            <div className="date-time d-flex flex-row justify-content-between"><span>PONIEDZIAŁEK</span><span> Zniżka 10% na każdą kolejną godzinę</span>
                            </div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>WTOREK</span><span>Studencikie wtorki -30% na bar dla studentów</span>
                            </div>
                            <div className="date-time d-flex flex-row justify-content-between">
                                <span>ŚRODA</span><span>-----------------------</span>
                            </div>
                            <div className="date-time d-flex flex-row justify-content-between">
                                <span>CZWARTEK</span><span>-----------------------</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>PIĄTEK</span><span>Trzy tory w cenie dwóch przy rezerwacji na min 2h</span>
                            </div>
                            <div className="date-time d-flex flex-row justify-content-between">
                                <span>SOBOTA</span><span>-----------------------</span>
                            </div>
                            <div className="date-time d-flex flex-row justify-content-between">
                                <span>NIEDZIELA</span><span>-----------------------</span></div>
                        </div>
                    </div>
                    <div className="box-2 baba-container flex-grow-1 d-flex">
                        <img className="baba flex-grow-1 w-100"
                             src={bowling_photo}
                             alt="bowling"/>
                    </div>
                    <div className="box-1 w-100 align-self-stretch">
                        <p>REGULAMIN KRĘGIELNI BOWLING</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            <div className="date-time d-flex flex-row justify-content-between">
                                {regulations.map((regulation, index) => (
                                    <div className="date-time d-flex flex-row justify-content-between"
                                         key={index}>
                                        <span>{regulation.number} {regulation.description}</span>
                                    </div>
                                ))}
                                <div className="date-time d-flex flex-row justify-content-between">
                                    <span>1. PUNKT PIERWSZY</span>
                                </div>
                                <div className="date-time d-flex flex-row justify-content-between">
                                    <span>1. PUNKT PIERWSZY</span>
                                </div><div className="date-time d-flex flex-row justify-content-between">
                                <span>1. PUNKT PIERWSZY</span>
                            </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}