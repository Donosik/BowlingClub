import './Header.css'
import React from 'react';
import {Link} from "react-router-dom";
import logo from "./logo-bowl.png"

export default function Header()
{
    const [isUserLoggedIn, setIsUserLoggedIn] = React.useState(isLoggedIn())
    function logout()
    {
        if (localStorage.getItem('token') !== null)
        {
            localStorage.removeItem('token')
            setIsUserLoggedIn(false)
        }
    }
    function isLoggedIn()
    {
        if(localStorage.getItem('token') !== null)
        {
            return true
        }
        return false
    }

    return (<div>
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
                    <li className="nav-item">
                        <Link className="nav-link"
                              to="/">HOME</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link"
                              to="/oferta">OFERTA</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link"
                              to="/kontakt">KONTAKT</Link>
                    </li>
                    {isUserLoggedIn ? (
                        <li className="nav-item">
                            <Link className="nav-link" to="/home" onClick={logout}>WYLOGUJ</Link>
                        </li>
                    ) : (
                        <li className="nav-item">
                            <Link className="nav-link"
                                  to="/login">
                                LOGOWANIE
                            </Link>
                        </li>
                    )}
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
    </div>);
}