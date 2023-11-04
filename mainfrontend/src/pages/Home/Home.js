import './home.css'
import bowling_photo from './pobrany plik.jpg'
export default function Home()
{
    return (
        <>
            <div className="container justify-content-center">
                <div className="row w-100 align-items-center">
                    <div className="col-4 row box-1">

                        <p>GODZINY OTWARCIA</p>

                        <p className="days">PONIEDZIAŁEK <span className="hours">16:00 - 22:00</span> </p>
                        <p className="days">WTOREK<span className="hours">16:00 - 22:00</span> </p>
                        <p className="days">ŚRODA <span className="hours">16:00 - 22:00</span></p>
                        <p className="days">CZWARTEK <span className="hours">16:00 - 22:00</span></p>
                        <p className="days">PIĄTEK <span className="hours">16:00 - 22:00</span></p>
                        <p className="days">SOBOTA <span className="hours">16:00 - 22:00</span></p>
                        <p className="days">NIEDZIELA<span className="hours">16:00 - 22:00</span></p>

                    </div>
                    <div className="col-8 justify-content-center box-2">
                        <div className="city-card">
                            <div className="activity-img">
                                <img src={bowling_photo} alt="bowling"/>

                            </div>
                        </div>
                    </div>
                </div>

                <div className="row w-100 align-items-center">
                    <div className="col-8 justify-content-center box-2">
                        <div className="city-card">
                            <div className="activity-img">
                                <img src={bowling_photo} alt="bowling"/>

                            </div>
                        </div>
                    </div>
                    <div className="col-4 row box-1">

                        <p>GODZINY OTWARCIA</p>

                        <p className="days">PONIEDZIAŁEK <p className="hours">16:00 - 22:00</p> </p>
                        <p className="days">WTOREK<p className="hours">16:00 - 22:00</p> </p>
                        <p className="days">ŚRODA <p className="hours">16:00 - 22:00</p></p>
                        <p className="days">CZWARTEK <p className="hours">16:00 - 22:00</p></p>
                        <p className="days">PIĄTEK <p className="hours">16:00 - 22:00</p></p>
                        <p className="days">SOBOTA <p className="hours">16:00 - 22:00</p></p>
                        <p className="days">NIEDZIELA<p className="hours">16:00 - 22:00</p></p>

                    </div>

                </div>
            </div>

        </>
    )
}