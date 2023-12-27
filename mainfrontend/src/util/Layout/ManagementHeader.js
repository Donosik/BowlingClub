import './Header.css'
import {Link, NavLink} from "react-router-dom";
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
                    <ul className="navbar-nav collapse navbar-collapse flex-grow-0"
                        id={"navbarToggleExternalContent"}>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/uzytkownicy"
                                     activeClassName="active">UŻYTKOWNICY</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/magazyn"
                                     activeClassName="active">MAGAZYN</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/sprzedaz"
                                     activeClassName="active">SPRZEDAŻ</NavLink>
                        </li>

                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/rezerwacje"
                                     activeClassName="active">REZERWACJE</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/grafik"
                                     activeClassName="active">GRAFIK</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/raporty"
                                     activeClassName="active">RAPORTY</NavLink>
                        </li>


                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management/zmianastrony"
                                     activeClassName="active">ZMIANA STRONY</NavLink>
                        </li>
                       {isUserLoggedIn() === true ? (
                            <li className="nav-item">
                                <Link className="nav-link"
                                      to="/home"
                                      onClick={logout}>WYLOGUJ</Link>
                            </li>
                        ) : null}

                    </ul>
                    <button className="navbar-toggler"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target="#navbarToggleExternalContent"
                            aria-controls="navbarToggleExternalContent"
                            aria-expanded="false"
                            aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                </div>
            </nav>
        </div>
    )
}