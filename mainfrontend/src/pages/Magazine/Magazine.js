import MagazineTable from "./MagazineTable";
import "./Magazine.css";
import lupa from "./Vector.png"
import logo from "../../util/Layout/logo-bowl.png";
import React from "react";

export default function Magazine()
{
    const products=[
        {
            Id:1,
            Name:"Kula do kręgli",
        },
    ]
    return(
        <>
            <div className="magazine-container">
                <div className="table-name">MAGAZYN PRZEDMIOTÓW</div>
           <button>MAGAZYN SPRZĘTU</button>
            <button>MAGAZYN BARU</button> <br/>

            <input/><img src={lupa} alt="lupa"/><button>DODAJ NOWY ARTUKUŁ
            </button><br/>
            <MagazineTable products={products}/> </div>
        </>
    )
}