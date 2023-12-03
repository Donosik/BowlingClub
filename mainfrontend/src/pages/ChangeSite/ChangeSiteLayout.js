import {Outlet, useNavigate} from "react-router-dom";

export default function ChangeSiteLayout()
{
    const navigate = useNavigate()
    return (
        <>
            <div>
            <button type="button"
                    onClick={() => navigate('godziny')}>Zmiana godzin
            </button>
            <button type="button"
                    onClick={() => navigate('promocje')}>Zmiana promocji
            </button>
            <button type="button"
                    onClick={() => navigate('regulamin')}>Zmiana regulaminu
            </button>
            </div>
            <Outlet/>
        </>
    )
}