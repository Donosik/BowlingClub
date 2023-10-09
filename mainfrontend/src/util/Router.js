import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import Layout from "./Layout";
import NoMatch from "../pages/NoMatch/NoMatch";
import LoginForm from "../pages/Login/LoginForm";
import RegisterForm from "../pages/Login/RegisterForm";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/Test"} element={<RegisterForm/>}/>
                <Route path={"/"} element={<Layout/>}>
                    <Route index element={<Home/>}/>
                    <Route path={"login"} element={<LoginForm/>}/>
                    <Route path={"*"} element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}