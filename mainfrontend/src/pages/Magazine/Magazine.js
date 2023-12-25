import MagazineTable from "./MagazineTable";
import "./Magazine.css";
import lupa from "./Vector.png"
import logo from "../../util/Layout/logo-bowl.png";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {mainBackendApi} from "../../util/Requests";

export default function Magazine()
{
    const[filter, setFilter] = useState("")
    const navigate = useNavigate();
    return (
        <>
            <div className="magazine-container">
                <div className="table-name">MAGAZYN</div>

                <input type={"text"} onChange={e=>setFilter(e.target.value)}/><img src={lupa}
                             alt="lupa"/>
                <button onClick={e => navigate("dodaj")}>DODAJ NOWY PRZEDMIOT
                </button>
                <br/>
                <MagazineTable filter={filter}/></div>
        </>
    )
}