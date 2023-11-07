import './Header.css'
import {Link} from "react-router-dom";
import logo from "./logo-bowl.png";
import React from "react";

export default function ManagementHeader()
{
    return(
        <nav className="container-style sticky-top navbar navbar-expand-lg">
            <div className="menu-link-text container">
                <Link className="bowling-text navbar-text" to="/management">
                    <div className="logo">

                        <img src={logo} alt="logo"/></div>
                </Link>
                <ul className="navbar-nav ml-auto">
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
                </ul>
            </div>
        </nav>
    )
}