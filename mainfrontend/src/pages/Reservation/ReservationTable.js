import React, {useEffect, useState} from 'react';
import {mainBackendApi} from "../../util/Requests";
import {useNavigate} from "react-router-dom";
import {getIsAdmin, getIsWorker, isUserLoggedIn} from "../../util/UserType";
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

pdfMake.vfs = pdfFonts.pdfMake.vfs;

export default function ReservationTable()
{
    const [reservations, setReservations] = useState([])
    const [currentPage, setCurrentPage] = useState(1);
    const [usersPerPage] = useState(100);
    const [noMoreUsers, setNoMoreUsers] = useState(false);
    const [isClient, setIsClient] = useState(true)
    const [onlyNewReservations, setOnlyNewReservations] = useState(true);
    const [onlyUnrealizedReservations, setOnlyUnrealizedReservations] = useState(getIsWorker());
    const [shouldFetch, setShouldFetch] = useState(false);
    const navigate=useNavigate()

    useEffect(() =>
    {
        if (isUserLoggedIn() === true && getIsWorker() === false && getIsAdmin() === false)
        {
            setIsClient(true)
            fetchReservationsForClient()
        }
        else
        {
            setIsClient(false)
            fetchReservations()
        }
    }, []);

    useEffect(() =>
    {
        if (shouldFetch)
        {
            if (isClient)
            {
                fetchReservationsForClient()
            }
            else
            {
                fetchReservations()
            }
            setShouldFetch(false)
        }
    }, [shouldFetch])

    async function reset()
    {
        setCurrentPage(1)
        setReservations([])
        setShouldFetch(true)
    }

    function extractDatePart(fullDate)
    {
        return fullDate.slice(0, 10);
    }

    function countOccurrences(inventory)
    {
        const occurrences = {}
        inventory.forEach((item) =>
        {
            if (!occurrences[item.name])
            {
                occurrences[item.name] = 1
            }
            else
            {
                occurrences[item.name]++
            }
        })

        const result = Object.keys(occurrences).map((name) => ({
            name,
            count: occurrences[name],
        }))
        return result
    }

    async function fetchReservations()
    {
        try
        {
            const response = await mainBackendApi.get('/Reservation/GetAllReservations/' + usersPerPage + '/' + currentPage + '/' + onlyNewReservations + '/' + onlyUnrealizedReservations)
            const data = response.data
            if (data.length === 0)
            {
                setNoMoreUsers(true)
            }
            else
            {
                setNoMoreUsers(false)
                setReservations([...reservations, ...data])
                setCurrentPage(currentPage + 1);
            }
        } catch (e)
        {
            console.log(e)
        }
    }

    async function fetchReservationsForClient()
    {
        try
        {
            const response = await mainBackendApi.get('/Reservation/GetClientReservations/' + usersPerPage + '/' + currentPage + '/' + onlyNewReservations + '/' + onlyUnrealizedReservations)
            const data = response.data
            if (data.length === 0)
            {
                setNoMoreUsers(true)
            }
            else
            {
                setNoMoreUsers(false)
                setReservations([...reservations, ...data])
                setCurrentPage(currentPage + 1);
            }
        } catch (e)
        {
            console.log(e)
        }
    }

    const generateFakturaDefinition = (sale) =>
    {
        const content = countOccurrences(sale.inventories).map((occurrence) =>
        {
            const itemDetails = sale.inventories.find((item) => item.name === occurrence.name);

            return [
                {text: occurrence.name, style: 'text'},
                {text: occurrence.count, style: 'text'},
                {text: itemDetails.price, style: 'text'},
                {text: itemDetails.price * occurrence.count, style: 'text'},
            ];
        });

        const tableBody = [
            [{text: 'Nazwa', style: 'tableHeader'}, {text: 'Ilość', style: 'tableHeader'}, {
                text: 'Cena za jednostkę',
                style: 'tableHeader'
            }, {text: 'Cena za całość', style: 'tableHeader'}],
            ...content,
            [
                { text: 'Razem:', colSpan: 3, alignment: 'right', bold: true, style: 'tableHeader' },
                {},
                {},
                { text: sale.amount, bold: true, style: 'tableHeader' },
            ],
        ]
        return {
            content: [
                {text: 'FAKTURA', style: 'header'},
                {text: '\n'}, // Dodaj odstęp
                {
                    table: {
                        headerRows: 1,
                        widths: ['*', '*', '*'],
                        body: [
                            ['Dane sprzedawcy', 'Nazwa firmy', 'Adres firmy'],
                            ['Jan KOWALSKI', 'Kręgielnia Bowling', 'Ul. Kręglicka 7\n43-210 Kręgostan'],
                        ],
                    },
                    layout: {
                        hLineWidth: function (i, node)
                        {
                            return i === 0 || i === node.table.body.length ? 2 : 1;
                        },
                        vLineWidth: function (i, node)
                        {
                            return 1;
                        },
                    },
                },
                {text: '\n'}, // Dodaj odstęp


                {text: 'Dane klienta:', style: 'subheader'},

                {
                    text: `Imię i nazwisko: ${sale.client.person.firstName} ${sale.client.person.lastName}`,
                    style: 'text',
                },
                {text: 'Szczegóły sprzedaży:', style: 'subheader'},
                {text: `Numer faktury: ${sale.id}`, style: 'text'},
                {text: `Data wystawienia: ${extractDatePart(sale.issueDate)}`, style: 'text'},
                {text: `Data zapłaty: ${extractDatePart(sale.dueDate)}`, style: 'text'},
                {text: 'Zawartość:', style: 'subheader'},
                {
                    table: {
                        headerRows: 1,
                        widths: ['*', '*', '*', '*'],
                        body: tableBody,
                    },
                },
            ],
            styles: {
                header: {
                    fontSize: 18,
                    bold: true,
                    margin: [0, 0, 0, 10],
                },
                subheader: {
                    fontSize: 14,
                    bold: true,
                    margin: [0, 10, 0, 5],
                },
                text: {
                    fontSize: 12,
                    margin: [0, 0, 0, 5],
                },
            },
        };
    };

    const handleFakturaClick = (sale) =>
    {
        const documentDefinition = generateFakturaDefinition(sale);
        pdfMake.createPdf(documentDefinition).download(`faktura_${sale.id}_${sale.client.person.firstName}_${sale.client.person.lastName}.pdf`);
    };

    return (
        <div className="table-container">
            <div>
                <label>TYLKO NOWE REZERWACJE</label>
                <input type={"checkbox"}
                       onChange={(e) =>
                       {
                           setOnlyNewReservations(e.target.checked)
                           reset()
                       }}
                       checked={onlyNewReservations}/>
            </div>
            <div>
                <label>TYLKO NIEZREALIZOWANE REZERWACJE</label>
                <input type={"checkbox"}
                       onChange={(e) =>
                       {
                           setOnlyUnrealizedReservations(e.target.checked)
                           reset()
                       }}
                       checked={onlyUnrealizedReservations}/>
            </div>
            <div className="table-container">
                <table className="table-bordered">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Data rezerwacji</th>
                        <th>Początek rezerwacji</th>
                        <th>Koniec rezerwacji</th>
                        <th>Tor</th>
                        <th>Id klienta</th>
                        <th>Imię klienta</th>
                        <th>Nazwisko klienta</th>
                        <th>Faktura</th>
                    </tr>
                    </thead>
                    <tbody>
                    {reservations.map(reservation => (
                        <tr key={reservation.id}>
                            <td>{reservation.id}</td>
                            <td>{reservation.startTime.slice(0,10)}</td>
                            <td>{reservation.startTime.slice(11,16)}</td>
                            <td>{reservation.endTime.slice(11,16)}</td>
                            <td>{reservation.lane.laneNumber}</td>
                            <td>{reservation.client.user.id} </td>
                            <td>{reservation.client.person.firstName} </td>
                            <td>{reservation.client.person.lastName} </td>
                            <td>{reservation.invoice ? (
                                <button onClick={() => handleFakturaClick(reservation.invoice)}>Faktura</button>) : (
                                (isClient === true) ?
                                    (
                                        'BRAK'
                                    )
                                    :
                                    (<button onClick={(e) =>
                                    {
                                        navigate('/management/sprzedaz/dodaj/'+reservation.id)
                                    }}>STWÓRZ FAKTURĘ</button>)
                            )}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
            <button onClick={(e) =>
            {
                isClient === true ? fetchReservationsForClient() : fetchReservations()
            }}>ZAŁADUJ DALEJ REZERWACJE
            </button>
            {(noMoreUsers === true) && <div className="no-more-users">ZAŁADOWANO JUŻ WSZYSTKIE</div>}
        </div>

    );
}