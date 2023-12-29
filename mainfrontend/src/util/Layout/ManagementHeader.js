import './Header.css'
import {Link, NavLink} from "react-router-dom";
import logo from "./logo-bowl.png";
import React, {useEffect} from "react";
import {getIsAdmin, getIsWorker} from "../UserType";

export default function ManagementHeader()
{
    const [isAdmin, setIsAdmin] = React.useState(false)
    const [isWorker, setIsWorker] = React.useState(false)

    useEffect(() =>
    {
        setIsAdmin(getIsAdmin)
        setIsWorker(getIsWorker)
    }, []);

    function logout()
    {
        if (localStorage.getItem('token') !== null)
        {
            localStorage.removeItem('token')
            setIsAdmin(false)
            setIsWorker(false)
        }
    }

    function isUserLoggedIn()
    {
        if (localStorage.getItem('token') !== null)
        {
            return true
        }
        return false
    }

    return (
        <div>
            <nav className="container-style sticky-top navbar navbar-expand-lg">
                <div className="menu-link-text container">
                    <Link className="bowling-text navbar-text"
                          to="/">
                        <div className="logo">

                            <img src={logo}
                                 alt="logo"/></div>
                    </Link>
                    <ul className="navbar-nav collapse navbar-collapse flex-grow-0"
                        id={"navbarToggleExternalContent"}>
                        {isAdmin === true &&
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/management/uzytkownicy"
                                         activeClassName="active">UŻYTKOWNICY</NavLink>
                            </li>}
                        {(isAdmin === true || isWorker === true) &&
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/management/magazyn"
                                         activeClassName="active">MAGAZYN</NavLink>
                            </li>}
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
                        {(isAdmin === true || isWorker === true) &&
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/management/grafik"
                                         activeClassName="active">GRAFIK</NavLink>
                            </li>}
                        {(isAdmin === true || isWorker === true) &&
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/management/raporty"
                                         activeClassName="active">RAPORTY</NavLink>
                            </li>}
                        {(isAdmin === true || isWorker === true) &&
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/management/zmianastrony"
                                         activeClassName="active">ZMIANA STRONY</NavLink>
                            </li>}
                        {isUserLoggedIn() === true &&
                            <li className="nav-item">
                                <Link className="nav-link"
                                      to="/home"
                                      onClick={logout}>WYLOGUJ</Link>
                            </li>}

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