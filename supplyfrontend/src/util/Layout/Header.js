import './header.css'
import React, {useEffect, useState} from 'react';
import {Link, NavLink} from "react-router-dom";
import {getIsLogged} from "../UserUtil";

export default function Header()
{
    const [isLogged,setIsLogged]=useState(false)
    useEffect(() =>
    {
        setIsLogged(getIsLogged())
    }, []);
    function logout()
    {
        if (localStorage.getItem('tokenSupply') !== null)
        {
            localStorage.removeItem('tokenSupply')
            setIsLogged(false)
        }
    }

    return (
        <div>
            <nav className="container-style sticky-top navbar navbar-expand-lg">
                <div className="menu-link-text container">
                    <ul className="navbar-nav collapse navbar-collapse flex-grow-0"
                        id={"navbarToggleExternalContent"}>
                        {(isLogged === true) && (
                            <>
                                <li className="nav-item">
                                    <NavLink className="nav-link"
                                             to="/">HOME</NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink className="nav-link"
                                             to="/zamowienia">ZAMÓWIENIA</NavLink>
                                </li>
                            </>
                        )}
                        {(isLogged === true) ? (
                            <li className="nav-item">
                                <NavLink className="nav-link"
                                         to="/login"
                                         onClick={logout}>WYLOGUJ</NavLink>
                            </li>
                        ) : (
                            <>
                                <li className="nav-item">
                                    <NavLink className="nav-link"
                                             to="/login">LOGIN</NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink className="nav-link"
                                             to="/rejestracja">REJESTRACJA</NavLink>
                                </li>
                            </>)}
                    </ul>
                </div>
            </nav>
        </div>
    )
}