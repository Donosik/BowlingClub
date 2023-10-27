import './Header.css'
import React from 'react';

export default function Header() {
    return (

        <nav className="container-style navbar navbar-expand-lg ">
            <div className="container">
                <a className="bowling-text navbar-text" href="#">
                   BOWLING
                </a>
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <a className="nav-link" href="#">HOME</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#">OFERTA</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#">KONTAKT</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link" href="#">LOGOWANIE</a>
                    </li>
                </ul>
            </div>
        </nav>

    );
}