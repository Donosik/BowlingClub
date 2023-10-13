import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import Layout from "./Layout";
import NoMatch from "../pages/NoMatch/NoMatch";
import LoginForm from "../pages/Login/LoginForm";
import RegisterForm from "../pages/Login/RegisterForm";
import LoginPage from "../pages/Login/LoginPage";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/Test"}
                       element={<LoginForm/>}/>
                <Route path={"/"}
                       element={<Layout/>}>
                    <Route index
                           element={<Home/>}/>
                    <Route path={"login"}
                           element={<LoginPage/>}/>
                    <Route path={"*"}
                           element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}