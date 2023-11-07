import {Outlet} from "react-router-dom";
import Footer from "./Footer";
import ManagementHeader from "./ManagementHeader";

export default function ManagementLayout()
{
    return(
        <>
            <ManagementHeader/>
            <Outlet/>
            <Footer/>
        </>
    )
}