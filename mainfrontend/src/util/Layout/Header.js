import './Header.css'
import React from 'react';
import {Link} from "react-router-dom";
import logo from "./logo-bowl.png"
import google from "../../pages/Login/google_icon.png";

export default function Header() {
    return (

        <nav className="container-style sticky-top navbar navbar-expand-lg">
            <div className="menu-link-text container">
                <Link className="bowling-text navbar-text" to="/">
                    <div className="logo">

                    <img src={logo} alt="logo"/></div>
                </Link>
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <Link className="nav-link" to="/">HOME</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/oferta">OFERTA</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/kontakt">KONTAKT</Link>
                    </li>
                    <li className="nav-item">
                        <Link className="nav-link" to="/login">LOGOWANIE</Link>
                    </li>
                </ul>
            </div>
        </nav>

    );
}