import './Header.css'
import React from 'react';

export default function Header() {
    return (
        <div className="container">
            <div className='header'>
                <div className='nagwek'>
                    <div className='text'>
                        <span>KRĘGIELNIA</span>
                        <br />
                        <span></span>
                    </div>
                    <span className='text05'>
                        <span>BOWLING</span>
                    </span>
                </div>
                <div className='menuugryzprzyciskami'>
                    <a href={''} className='text07 navbar-toggler'>
                        <span>LOGOWANIE</span>
                    </a>
                    <a href={''} className='text09'>
                        <span>KONTAKT</span>
                    </a>
                    <a href={''} className='text11'>
                        <span>OFERTA</span>
                    </a>
                    <a href={''} className='text13'>
                        <span>HOME</span>
                    </a>
                </div>
            </div>
        </div>
    );
}
