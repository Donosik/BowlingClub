import './Header.css'
import {Link, NavLink} from "react-router-dom";
import logo from "./logo-bowl.png";
import React, {useEffect} from "react";

export default function ManagementHeader()
{
    const [isAdmin, setIsAdmin] = React.useState(false)
    const [isWorker, setIsWorker] = React.useState(false)

    useEffect(() =>
    {
        if (localStorage.getItem('token') !== null)
        {
            const token = localStorage.getItem('token')
            const claims=parseJwt(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
            if(claims[2]&&claims[2]==="Admin")
            {
                setIsAdmin(true)
                setIsWorker(true)
            }
            else if(claims[1]&&claims[1]==="Worker")
            {
                setIsWorker(true)
                setIsAdmin(false)
            }
            else {
                setIsAdmin(false)
                setIsWorker(false)
            }
        }
    }, []);

    function parseJwt (token) {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    }
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