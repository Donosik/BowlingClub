import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import Layout from "./Layout";
import NoMatch from "../pages/NoMatch/NoMatch";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/"} element={<Layout/>}>
                    <Route index element={<Home/>}/>
                    <Route path={"*"} element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}