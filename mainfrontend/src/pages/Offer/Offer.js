import './offer.css'
import {mainBackendApi} from "../../util/Requests";
import {useEffect, useState} from "react";

export default function Offer()
{
    const [magazineOffer, setMagazineOffer] = useState([])

    useEffect(() =>
    {
        fetchMagazineOffer()
    }, []);

    async function fetchMagazineOffer()
    {
        try
        {
            const response = await mainBackendApi.get('TargetInventory/MagazineOffer')
            console.log(response.data)
            setMagazineOffer(response.data)
        } catch (e)
        {
            console.log(e)
        }
    }

    return (
        <>
            <div className="wrapper">
                <div className="left-panel">
                    <h2>Bar</h2>
                    <ul>
                        <table>
                            <thead>
                            <tr>
                                <th>NAZWA</th>
                                <th>CENA</th>
                            </tr>
                            </thead>
                            <tbody>
                            {magazineOffer.map((item) => (
                                <tr>
                                    <td>{item.name}</td>
                                    <td>{item.price+' zł'}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </ul>
                </div>
                <div className="right-panel">
                    <h2>Tory</h2>
                    <ul>
                        <li>Ceny gry w kręgle</li>
                        <li>Godziny otwarcia</li>
                        {/* Dodaj inne informacje */}
                    </ul>
                </div>
            </div>
        </>
    )
}