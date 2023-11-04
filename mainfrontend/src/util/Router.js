import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import ClientLayout from "./ClientLayout";
import NoMatch from "../pages/NoMatch/NoMatch";
import LoginForm from "../pages/Login/LoginForm";
import Login from "../pages/Login/Login";
import Contact from "../pages/Contact/Contact";
import Offer from "../pages/Offer/Offer";
import ManagementLayout from "./ManagementLayout";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/test"}
                       element={<LoginForm/>}/>
                <Route path={"/management"}
                       element={<ManagementLayout/>}>
                    <Route index
                           element={<LoginForm/>}/>
                </Route>
                <Route path={"/"}
                       element={<ClientLayout/>}>
                    <Route index
                           element={<Home/>}/>
                    <Route path={"home"}
                           element={<Home/>}/>
                    <Route path={"login"}
                           element={<Login/>}/>
                    <Route path={"kontakt"}
                           element={<Contact/>}/>
                    <Route path={"oferta"}
                           element={<Offer/>}/>
                    <Route path={"*"}
                           element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}