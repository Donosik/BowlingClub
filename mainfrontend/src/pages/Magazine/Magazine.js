import MagazineTable from "./MagazineTable";
import "./Magazine.css";
import lupa from "./Vector.png"
import logo from "../../util/Layout/logo-bowl.png";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {mainBackendApi} from "../../util/Requests";

export default function Magazine()
{
    const navigate = useNavigate();
    const[products,setProducts]=useState([])

    useEffect(() =>
    {
        fetchProducts()
    }, []);

    async function fetchProducts()
    {
        try
        {
            const response = await mainBackendApi.get('TargetInventory/MagazineStatus')
            setProducts(response.data)
        } catch (error)
        {
            console.log(error)
        }
    }
    return (
        <>
            <div className="magazine-container">
                <div className="table-name">MAGAZYN PRZEDMIOTÓW</div>

                <input/><img src={lupa}
                             alt="lupa"/>
                <button onClick={e => navigate("dodaj")}>DODAJ NOWY PRZEDMIOT
                </button>
                <br/>
                <MagazineTable products={products}/></div>
        </>
    )
}