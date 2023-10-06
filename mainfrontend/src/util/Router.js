import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import Layout from "./Layout";
import NoMatch from "../pages/NoMatch/NoMatch";
import Login from "../pages/Login/Login";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/Test"} element={<Login/>}/>
                <Route path={"/"} element={<Layout/>}>
                    <Route index element={<Home/>}/>
                    <Route path={"login"} element={<Login/>}/>
                    <Route path={"*"} element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}