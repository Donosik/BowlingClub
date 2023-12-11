import './Header.css'
import {Link} from "react-router-dom";
import logo from "./logo-bowl.png";
import React from "react";

export default function ManagementHeader()
{

    function logout()
    {
        if (localStorage.getItem('token') !== null)
        {
            localStorage.removeItem('token')
        }
    }
    function isUserLoggedIn()
    {
       if(localStorage.getItem('token') !== null)
       {
           return true
       }
       return false
    }

    return(
        <div>
            <nav className="container-style sticky-top navbar navbar-expand-lg">
                <div className="menu-link-text container">
                    <Link className="bowling-text navbar-text" to="/management">
                        <div className="logo">

                            <img src={logo} alt="logo"/></div>
                    </Link>
                    <ul className="navbar-nav collapse navbar-collapse flex-grow-0" id={"navbarToggleExternalContent"}>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/uzytkownicy">UŻYTKOWNICY</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/magazyn">MAGAZYN</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/sprzedaz">SPRZEDAŻ</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/zmianastrony">ZMIANA STRONY</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/rezerwacje">REZERWACJE</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/grafik">GRAFIK</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/raporty">RAPORTY</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/management/systemzakupow">SYSTEM ZAKUPÓW</Link>
                        </li>
                        {isUserLoggedIn() === true ? (
                            <li className="nav-item">
                                <Link className="nav-link" to="/home" onClick={logout}>WYLOGUJ</Link>
                            </li>
                        ):null}
                    </ul>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                            data-bs-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                </div>
            </nav>
        </div>
    )
}