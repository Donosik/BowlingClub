import './Header.css'
import React, {useEffect} from 'react';
import {Link, NavLink} from "react-router-dom";
import logo from "./logo-bowl.png"
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../UserType";

export default function Header()
{
    const [isLogged, setIsLogged] = React.useState(false)
    const [isAdmin, setIsAdmin] = React.useState(false)
    const [isWorker, setIsWorker] = React.useState(false)

    useEffect(() =>
    {
        setIsLogged(isUserLoggedIn())
        setIsAdmin(getIsAdmin())
        setIsWorker(getIsWorker())
    }, []);

    function logout()
    {
        if (localStorage.getItem('token') !== null)
        {
            localStorage.removeItem('token')
            setIsLogged(false)
        }
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
                        <NavLink className="nav-link"
                                 to="/"
                                 exact
                                 activeClassName="active">HOME</NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink className="nav-link"
                                 to="/oferta"
                                 activeClassName="active">OFERTA</NavLink>
                    </li>
                    <li className="nav-item">
                        <NavLink className="nav-link"
                                 to="/kontakt"
                                 activeClassName="active">KONTAKT</NavLink>
                    </li>
                    {(isLogged === true && isAdmin === false && isWorker === false) &&
                        <>
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/sprzedaz"
                                         activeClassName="active">SPRZEDAŻ</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/rezerwacje"
                                         activeClassName="active">REZERWACJE</NavLink>
                            </li>
                        </>}

                    {(isLogged === true && (isAdmin === true || isWorker === true)) &&
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/management"
                                     activeClassName="active">ZARZĄDZANIE</NavLink>
                        </li>}

                    {isLogged === true ? (
                        <>
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/profil">PROFIL</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/home"
                                         onClick={logout}>WYLOGUJ</NavLink>
                            </li>
                        </>
                    ) : (
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/login"
                                     activeClassName="active">LOGOWANIE</NavLink>
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