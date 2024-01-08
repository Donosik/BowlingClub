import axios from "axios";


export const mainBackendApi = axios.create({
    baseURL: 'https://bowlingclub.azurewebsites.net/',
    headers:
        {
            'Content-Type': 'application/json'
        }
})


mainBackendApi.interceptors.request.use(config =>
{
    const token = localStorage.getItem('token');
    if (token)
    {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export function setAuth(token)
{
    if (token)
    {
        localStorage.setItem('token', token)
    }
}