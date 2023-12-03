import {BrowserRouter, Route, Routes} from "react-router-dom";
import Home from "../pages/Home/Home";
import ClientLayout from "./Layout/ClientLayout";
import NoMatch from "../pages/NoMatch/NoMatch";
import LoginForm from "../pages/Login/LoginForm";
import Login from "../pages/Login/Login";
import Contact from "../pages/Contact/Contact";
import Offer from "../pages/Offer/Offer";
import ManagementLayout from "./Layout/ManagementLayout";
import Users from "../pages/Users/Users";
import Magazine from "../pages/Magazine/Magazine";
import ChangeOpenHours from "../pages/ChangeSite/ChangeOpenHours";
import Reservation from "../pages/Reservation/Reservation";
import Schedule from "../pages/Schedule/Schedule";
import Raports from "../pages/Raports/Raports";
import PurchaseSystem from "../pages/PurchaseSystem/PurchaseSystem";
import Sale from "../pages/Sale/Sale";
import AddWorker from "../pages/Users/AddWorker";
import EditUser from "../pages/Users/EditUser";
import ChangeRegulations from "../pages/ChangeSite/ChangeRegulations";
import ChangeSiteLayout from "../pages/ChangeSite/ChangeSiteLayout";
import ChangePromotions from "../pages/ChangeSite/ChangePromotions";
import AddRegulation from "../pages/ChangeSite/AddRegulation";

export default function Router()
{
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/test"} element={<LoginForm/>}/>
                <Route path={"/management"} element={<ManagementLayout/>}>
                    <Route index element={<Home/>}/>
                    <Route path={"uzytkownicy"}>
                        <Route index element={<Users/>}/>
                        <Route path={"dodaj"} element={<AddWorker/>}/>
                        <Route path={"edytuj/:id"} element={<EditUser/>}/>
                    </Route>
                    <Route path={"magazyn"} element={<Magazine/>}/>
                    <Route path={"sprzedaz"} element={<Sale/>}/>
                    <Route path={"zmianastrony"} element={<ChangeSiteLayout/>}>
                        <Route index element={<ChangeOpenHours/>}/>
                        <Route path={"godziny"} element={<ChangeOpenHours/>}/>
                        <Route path={"promocje"} element={<ChangePromotions/>}/>
                        <Route path={"regulamin"}>
                            <Route index element={<ChangeRegulations/>}/>
                            <Route path={"dodaj"} element={<AddRegulation/>}/>
                        </Route>
                    </Route>
                    <Route path={"rezerwacje"} element={<Reservation/>}/>
                    <Route path={"grafik"} element={<Schedule/>}/>
                    <Route path={"raporty"} element={<Raports/>}/>
                    <Route path={"systemzakupow"} element={<PurchaseSystem/>}/>
                </Route>
                <Route path={"/"} element={<ClientLayout/>}>
                    <Route index element={<Home/>}/>
                    <Route path={"home"} element={<Home/>}/>
                    <Route path={"login"} element={<Login/>}/>
                    <Route path={"kontakt"} element={<Contact/>}/>
                    <Route path={"oferta"} element={<Offer/>}/>
                    <Route path={"*"} element={<NoMatch/>}/>
                </Route>
            </Routes>
        </BrowserRouter>
    )
}