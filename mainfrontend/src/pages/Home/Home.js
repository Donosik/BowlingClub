import './home.css'
import bowling_photo from './pobrany plik.jpeg'
export default function Home()
{
    return (
        <>
            <div className="container d-flex w-100 justify-content-center align-content-center content-container">
                <div className="d-flex align-items-center flex-row flex-wrap time-baba justify-content-between">
                    <div className="box-1 flex-grow-1">
                        <p>GODZINY OTWARCIA</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            <div className="date-time d-flex flex-row justify-content-between"><span>PONIEDZIAŁEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>WTOREK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>ŚRODA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>CZWARTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>PIĄTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>SOBOTA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>NIEDZIELA</span><span>16:00 - 22:00</span></div>
                        </div>
                    </div>
                    <div className="box-1 flex-grow-1">
                        <p>GODZINY OTWARCIA</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            <div className="date-time d-flex flex-row justify-content-between"><span>PONIEDZIAŁEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>WTOREK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>ŚRODA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>CZWARTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>PIĄTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>SOBOTA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>NIEDZIELA</span><span>16:00 - 22:00</span></div>
                        </div>
                    </div>
                    <div className="box-2 baba-container flex-grow-1 d-flex">
                        <img className="baba flex-grow-1 w-100" src={bowling_photo} alt="bowling"/>
                    </div>
                    <div className="box-1 w-100 align-self-stretch">
                        <p>GODZINY OTWARCIA</p>

                        <div className="date-time-container d-flex flex-column justify-content-between">
                            <div className="date-time d-flex flex-row justify-content-between"><span>PONIEDZIAŁEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>WTOREK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>ŚRODA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>CZWARTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>PIĄTEK</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>SOBOTA</span><span>16:00 - 22:00</span></div>
                            <div className="date-time d-flex flex-row justify-content-between"><span>NIEDZIELA</span><span>16:00 - 22:00</span></div>
                        </div>
                    </div>
                </div>
            </div>

        </>
    )
}