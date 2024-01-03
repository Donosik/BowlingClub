import React from "react";
import {BrowserRouter, Route, Routes} from "react-router-dom";
import Layout from "./Layout/Layout";
import Home from "../components/Home/Home";
import Login from "../components/Login/Login";
import Register from "../components/Login/Register";
import Orders from "../components/Orders/Orders";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/"}
                       element={<Layout/>}>
                    <Route index
                           element={<Home/>}/>
                    <Route path={"Home"}
                           element={<Home/>}/>
                    <Route path={"Login"}
                           element={<Login/>}/>
                    <Route path={"Rejestracja"}
                           element={<Register/>}/>
                    <Route path={"Zamowienia"}
                           element={<Orders/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}