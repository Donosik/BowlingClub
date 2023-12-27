import axios from "axios";

export const supplyBackendApi = axios.create({
    baseURL:'https://localhost:44373/',
    headers:
        {
            'Content-Type': 'application/json'
        }
})
