import React, { useEffect, useState } from "react"
import { mainBackendApi } from "../../util/Requests"
import { useNavigate } from "react-router-dom"

export default function ChangeRegulations() {
    const [regulations, setRegulations] = useState([])
    const navigate = useNavigate()

    useEffect(() => {
        fetchRegulations()
    }, [])

    async function fetchRegulations() {
        try {
            const response = await mainBackendApi.get("Regulation")
            const data = response.data
            setRegulations(data)
        } catch (error) {
            console.error("Error fetching regulations:", error)
        }
    }

    function handleInputChange(index, field, value) {
        const updatedRegulations = [...regulations]
        updatedRegulations[index] = {
            ...updatedRegulations[index],
            [field]: value,
        }
        setRegulations(updatedRegulations)
    }

    async function handleSaveChanges(e) {
        e.preventDefault()
        try {
            await mainBackendApi.put("Regulation", regulations)
        } catch (error) {
            console.error("Error updating regulations:", error)
        }
        await fetchRegulations()
    }

    async function deleteRegulation(id) {
        try {
            await mainBackendApi.delete("Regulation/" + id)
        } catch (error) {
            console.error("Error deleting regulations:", error)
        }
        await fetchRegulations()
    }

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZMIANA ZASAD REGULAMINU</div>
                    <button type="button" onClick={() => navigate("dodaj")}>
                        Dodaj nowy punkt regulaminu
                    </button>
                    <div className="table-container">
                        <table className="table-bordered">
                            <thead>
                            <tr>
                                <th>Punkt regulaminu</th>
                                <th>Opis</th>
                                <th>Przyciski</th>
                            </tr>
                            </thead>
                            <tbody>
                            {regulations.map((regulation, index) => (
                                <tr key={regulation.id}>
                                    <td>{regulation.number}</td>
                                    <td>
                                        <input
                                            type="text"
                                            value={regulation.description}
                                            onChange={(e) =>
                                                handleInputChange(index, "description", e.target.value)
                                            }
                                        />
                                    </td>
                                    <td>
                                        <button onClick={()=>deleteRegulation(regulation.id)}>USUŃ</button>
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                    <div className="d-flex">
                        <button type="button" onClick={handleSaveChanges}>
                            ZAPISZ ZMIANY
                        </button>
                    </div>
                </form>
            </div>
        </>
    )
}