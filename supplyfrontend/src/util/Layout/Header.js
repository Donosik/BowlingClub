import './header.css'
import React from 'react';
import {Link, NavLink} from "react-router-dom";
import {getIsLogged} from "../UserUtil";

export default function Header()
{
    return (
        <div>
            <nav className="container-style sticky-top navbar navbar-expand-lg">
                <div className="menu-link-text container">
                    <ul className="navbar-nav collapse navbar-collapse flex-grow-0"
                        id={"navbarToggleExternalContent"}>
                        {(getIsLogged() === true) && (
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
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/login">LOGIN</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link"
                                     to="/rejestracja">REJESTRACJA</NavLink>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
    )
}