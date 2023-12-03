import React, {useEffect, useState} from "react"
import {mainBackendApi} from "../../util/Requests"

export default function ChangePromotions()
{
    const [promotions, setPromotions] = useState([])

    useEffect(() => {
        fetchPromotions()
    }, [])
    function getDayName(dayNumber)
    {
        const daysOfWeek = ['NIEDZIELA', 'PONIEDZIAŁEK', 'WTOREK', 'ŚRODA', 'CZWARTEK', 'PIĄTEK', 'SOBOTA']
        return daysOfWeek[dayNumber]
    }
    function handleInputChange(index, field, value) {
        const updatedPromotions = [...promotions]
        updatedPromotions[index] = {
            ...updatedPromotions[index],
            [field]: value,
        }
        setPromotions(updatedPromotions)
    }

    async function fetchPromotions() {
        try {
            const response = await mainBackendApi.get("Promotion")
            const data = response.data
            console.log(data)
            setPromotions(data)
        } catch (error) {
            console.error("Error fetching promotions:", error)
        }
    }
    async function createPromotions()
    {
        const response = await mainBackendApi.post('Promotion')
        console.log(response)
    }
    async function handleSaveChanges(e) {
        e.preventDefault()
        try {
            await mainBackendApi.put("Promotion", promotions)
        } catch (error) {
            console.error("Error updating regulations:", error)
        }
        await fetchPromotions()
    }

    return (
        <>
            <div className="table-container">
                <form>
                    <div className="table-name">ZMIANA PROMOCJI LOKALU</div>
                    <button type="button"
                            onClick={(e) => createPromotions()}>Stwórz domyślny harmonogram
                    </button>
                    <div className="table-container">
                        <table className="table-bordered">
                            <thead>
                            <tr>
                                <th>Dzień</th>
                                <th>Promocja</th>
                            </tr>
                            </thead>
                            <tbody>
                            {promotions.map((day, index) => (
                                <tr key={index}>
                                    <td>{getDayName(day.dayOfWeek)}</td>
                                    <td>
                                        <input
                                            type="text"
                                            value={day.description}
                                            onChange={(e) =>
                                                handleInputChange(index, 'description', e.target.value)
                                            }
                                        />
                                    </td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                    </div>
                    <div className="d-flex">
                        <button type="submit"
                                onClick={handleSaveChanges}>
                            ZAPISZ ZMIANY
                        </button>
                    </div>
                </form>
            </div>
        </>
    )
}