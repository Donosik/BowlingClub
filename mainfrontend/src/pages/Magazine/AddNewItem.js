import React, { useState } from "react";
import { mainBackendApi } from "../../util/Requests";
import { useNavigate } from "react-router-dom";

export default function AddNewItem() {
    const [name, setName] = useState("");
    const [price, setPrice] = useState("");
    const [targetAmount, setTargetAmount] = useState("");
    const navigate = useNavigate();
    const [errors, setErrors] = useState({
        name: "",
        price: "",
        targetAmount: "",
    });

    const handleNameChange = (e) => {
        const enteredName = e.target.value;
        setName(enteredName);
        setErrors((prevErrors) => ({ ...prevErrors, name: "" }));
    };

    const handlePriceChange = (e) => {
        const validPrice = e.target.value.replace(/[^0-9.,]/g, "");
        setPrice(validPrice);
        setErrors((prevErrors) => ({ ...prevErrors, price: "" }));
    };

    const handleTargetAmountChange = (e) => {
        const validAmount = e.target.value.replace(/[^0-9]/g, "");
        setTargetAmount(validAmount);
        setErrors((prevErrors) => ({ ...prevErrors, targetAmount: "" }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newErrors = {};

        if (!name.trim()) {
            newErrors.name = "Nazwa produktu nie może być pusta";
        }

        if (!price.trim()) {
            newErrors.price = "Cena jest wymagana";
        } else if (isNaN(price) || price < 0) {
            newErrors.price = "Podaj prawidłową cenę";
        }

        if (!targetAmount.trim()) {
            newErrors.targetAmount = "Ilość jest wymagana";
        } else if (isNaN(targetAmount) || targetAmount < 0) {
            newErrors.targetAmount = "Podaj prawidłową ilość";
        }

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        try {
            const requestData = {
                Name: name,
                Price: price,
                Quantity: targetAmount,
            };
            const response = await mainBackendApi.post("TargetInventory", requestData);
            navigate("/management/magazyn");
        } catch (error) {
            console.error(error);
            // Handle error
        }
    };

    return (
        <div className="magazine-container">
            <div className="container page d-flex justify-content-center align-items-center">
                <div className="row">
                    <div className="form-box">
                        <h1 className="text-login">DODAWANIE PRODUKTÓW</h1>
                        <br />
                        <label>
                            NAZWA PRODUKTU:
                            <br />
                            <input type={"text"} value={name} onChange={handleNameChange} />
                            {errors.name && <div className="error-message">{errors.name}</div>}
                        </label>
                        <br />
                        <label>
                            CENA W PLN:
                            <br />
                            <input type={"text"} value={price} onChange={handlePriceChange} />
                            {errors.price && <div className="error-message">{errors.price}</div>}
                        </label>
                        <br />
                        <label>
                            ILOŚĆ, JAKA POWINNA BYĆ W MAGAZYNIE:
                            <br />
                            <input
                                type={"number"}
                                value={targetAmount}
                                onChange={handleTargetAmountChange}
                            />
                            {errors.targetAmount && (
                                <div className="error-message">{errors.targetAmount}</div>
                            )}
                        </label>
                        <br />
                        <button onClick={handleSubmit}>DODAJ</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
