import axios from "axios";

export const supplyBackendApi = axios.create({
    baseURL:'https://suppliesbowlingclub.azurewebsites.net',
    headers:
        {
            'Content-Type': 'application/json'
        }
})

supplyBackendApi.interceptors.request.use(config=>{
    const token=localStorage.getItem("tokenSupply")
    if(token)
        config.headers.Authorization=`Bearer ${token}`
    return config
})

export function setAuth(token)
{
    if (token)
    {
        localStorage.setItem('tokenSupply', token)
    }
}