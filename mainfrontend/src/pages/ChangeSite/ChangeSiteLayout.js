import {Outlet, useNavigate} from "react-router-dom";
import './ChangeSite.css'
export default function ChangeSiteLayout()
{
    const navigate = useNavigate()
    return (
        <>
            <div className='margin-buttons'>
            <button type="button"
                    onClick={() => navigate('godziny')}>ZMIANA GODZIN
            </button>
            <button type="button"
                    onClick={() => navigate('promocje')}>ZMIANA PROMOCJI
            </button>
            <button type="button"
                    onClick={() => navigate('regulamin')}>ZMIANA REGULAMINU
            </button>
            </div>
            <Outlet/>
        </>
    )
}