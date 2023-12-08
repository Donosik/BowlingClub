import './home.css'
import bowling_photo from './haome_photo.jpg'
import bowls_photo from './photo_bowls.jpg'
import {useEffect, useState} from "react";
import {mainBackendApi} from "../../util/Requests";

export default function Home() {
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
    const [promotions, setPromotions] = useState([
        {
            "id": 0,
            "dayOfWeek": 1,
            "description": "Zniżka 10% na każdą kolejną godzinę",
        },
        {
            "id": 1,
            "dayOfWeek": 2,
            "description": "Studencikie wtorki -30% na bar dla studentów",
        },
        {
            "id": 2,
            "dayOfWeek": 3,
            "description": "-----------------------",
        },
        {
            "id": 3,
            "dayOfWeek": 4,
            "description": "-----------------------",
        },
        {
            "id": 4,
            "dayOfWeek": 5,
            "description": "Trzy tory w cenie dwóch przy rezerwacji na min 2h",
        },
        {
            "id": 5,
            "dayOfWeek": 6,
            "description": "-----------------------",
        },
        {
            "id": 6,
            "dayOfWeek": 0,
            "description": "-----------------------",
        },
    ])

    useEffect(() => {
        fetchOpeningHours()
        fetchRegulations()
        fetchPromotions()
    }, [])

    async function fetchOpeningHours() {
        try {
            const response = await mainBackendApi.get('Data')
            const data = response.data
            setOpeningHours(data)
        } catch (error) {
            console.error('Error fetching opening hours:', error)
        }
    }

    async function fetchPromotions() {
        try {
            const response = await mainBackendApi.get("Promotion")
            const data = response.data
            setPromotions(data)
        } catch (error) {
            console.error("Error fetching promotions:", error)
        }
    }

    async function fetchRegulations() {
        try {
            const response = await mainBackendApi.get("Regulation");
            const data = response.data;
            setRegulations(data);
        } catch (error) {
            console.error("Error fetching regulations:", error);
        }
    }


    function getDayName(dayNumber) {
        const daysOfWeek = ['NIEDZIELA', 'PONIEDZIAŁEK', 'WTOREK', 'ŚRODA', 'CZWARTEK', 'PIĄTEK', 'SOBOTA'];
        return daysOfWeek[dayNumber];
    }

    function getSmallHour(hour) {
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
                    <div className="box-2 baba-container flex-grow-1 d-flex">
                        <img className="baba flex-grow-1 w-100"
                             src={bowling_photo}
                             alt="bowling"/>
                    </div>
                    <div className="box-1 flex-grow-1">
                        <p>NASZE PROMOCJE</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            {promotions.map((day, index) => (
                                <div className="date-time d-flex flex-row justify-content-between"
                                     key={index}>
                                    <span>{getDayName(day.dayOfWeek)}</span>
                                    <span>{day.description}</span>
                                </div>
                            ))}
                        </div>
                    </div>

                    <div className="box-1 w-100 align-self-stretch">
                        <p>REGULAMIN KRĘGIELNI BOWLING</p>

                        <div className="datee-time-containr d-flex flex-column justify-content-between">

                            {regulations.map((regulation, index) => (
                                <div
                                    key={index}>
                                    <span>{regulation.number}. {regulation.description}</span><br/>
                                </div>
                            ))}


                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}