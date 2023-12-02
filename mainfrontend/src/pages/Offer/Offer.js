import './offer.css'
export default function Offer()
{
    return (
        <>
            <div className="wrapper">
                <div className="left-panel">
                    <h2>Bar</h2>
                    <ul>
                        <li>Oferta napojów</li>
                        <li>Ceny alkoholi</li>
                        {/* Dodaj inne informacje */}
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