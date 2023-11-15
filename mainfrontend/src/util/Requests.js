import axios from "axios";


export const mainBackendApi = axios.create({
    baseURL: 'https://localhost:44302/',
    headers:
        {
            'Content-Type': 'application/json'
        }
})


mainBackendApi.interceptors.request.use(config =>
{
    const token = localStorage.getItem('token');
    //console.log(token)
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
        console.log("token is set")
        console.log(token)
    }
}