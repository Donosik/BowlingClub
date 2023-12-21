import './header.css'
import React from 'react';
import {Link,NavLink} from "react-router-dom";
export default function Header () {
    return (
        <ul className="navbar-nav collapse navbar-collapse flex-grow-0"
            id={"navbarToggleExternalContent"}>
            <li className="nav-item">
                <NavLink className="nav-link"
                         to="/"
                         exact
                         activeClassName="active">HOME</NavLink>
            </li>
        </ul>
    )
}