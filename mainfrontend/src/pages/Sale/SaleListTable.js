import React, {useState} from 'react';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

pdfMake.vfs = pdfFonts.pdfMake.vfs;

export default function SaleListTable({sales})
{
    const handleFakturaClick = (sale) =>
    {
        const documentDefinition = generateFakturaDefinition(sale);
        pdfMake.createPdf(documentDefinition).download(`faktura_${sale.id}_${sale.client.person.firstName}_${sale.client.person.lastName}.pdf`);
    };

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

    function extractDatePart(fullDate)
    {
        return fullDate.slice(0, 10);
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
    return (
        <div className="table-container">
            <table className="table-bordered">
                <thead>
                <tr>
                    <th>ID</th>
                    <th>Data wystawienia</th>
                    <th>Data zapłaty</th>
                    <th>ID Klienta</th>
                    <th>Imię klienta</th>
                    <th>Nazwisko klienta</th>
                    <th>ID pracownika</th>
                    <th>Imię pracownika</th>
                    <th>Nazwisko pracownika</th>
                    <th>Zawartość</th>
                    <th>Kwota</th>
                    <th>Pobierz</th>
                </tr>
                </thead>
                <tbody>
                {sales.map((sale) => (
                    <tr key={sale.id}>
                        <td>{sale.id}</td>
                        <td>{extractDatePart(sale.issueDate)}</td>
                        <td>{extractDatePart(sale.dueDate)}</td>
                        <td>{sale.client.user.id}</td>
                        <td>{sale.client.person.firstName}</td>
                        <td>{sale.client.person.lastName}</td>
                        <td>{sale.worker.user.id}</td>
                        <td>{sale.worker.person.firstName}</td>
                        <td>{sale.worker.person.lastName}</td>
                        <td>{countOccurrences(sale.inventories).map((occurence) => (
                            <div>{occurence.name} - {occurence.count}</div>
                        ))}</td>
                        <td>{sale.amount}</td>
                        <td>
                            <button onClick={() => handleFakturaClick(sale)}>Faktura</button>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}
