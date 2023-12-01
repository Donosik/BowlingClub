import React, { useState } from 'react';
import pdfMake from 'pdfmake/build/pdfmake';
import pdfFonts from 'pdfmake/build/vfs_fonts';

pdfMake.vfs = pdfFonts.pdfMake.vfs;

export default function SaleListTable({ sales }) {
    const handleFakturaClick = (sale) => {
        const documentDefinition = generateFakturaDefinition(sale);
        pdfMake.createPdf(documentDefinition).download(`faktura_${sale.Id}.pdf`);
    };

    const generateFakturaDefinition = (sale) => {
        return {
            content: [
                { text: 'FAKTURA', style: 'header' },
                { text: 'Dane sprzedawcy:', style: 'subheader' },
                { text: 'Nazwa firmy', style: 'subheader' },
                { text: 'Adres firmy', style: 'subheader' },
                { text: 'Dane klienta:', style: 'subheader' },
                { text: `Imię i nazwisko: ${sale.Client.FirstName} ${sale.Client.LastName}`, style: 'text' },
                { text: `ID Klienta: ${sale.Client.Id}`, style: 'text' },
                { text: 'Szczegóły sprzedaży:', style: 'subheader' },
                { text: `ID: ${sale.Id}`, style: 'text' },
                { text: `Data wystawienia: ${sale.IssueDate}`, style: 'text' },
                { text: `Data zapłaty: ${sale.DueDate}`, style: 'text' },
                { text: 'Zawartość:', style: 'subheader' },
                { text: sale.content, style: 'text' },
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
                    <th>Pobierz</th>
                </tr>
                </thead>
                <tbody>
                {sales.map((sale) => (
                    <tr key={sale.Id}>
                        <td>{sale.Id}</td>
                        <td>{sale.IssueDate}</td>
                        <td>{sale.DueDate}</td>
                        <td>{sale.Client.Id}</td>
                        <td>{sale.Client.FirstName}</td>
                        <td>{sale.Client.LastName}</td>
                        <td>{sale.Worker.Id}</td>
                        <td>{sale.Worker.FirstName}</td>
                        <td>{sale.Worker.LastName}</td>
                        <td>{sale.content}</td>
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
