import axios from "axios";

export function mainBackendApi()
{
    const api=axios.create({
        baseURL:'https://localhost:44302/',
        headers:
            {
                'Content-Type':'application/json'
            }
    })
    return api
}

export function setAuth(token)
{
    if(token)
    {
        mainBackendApi().defaults.headers.common['Authorization'] = `Bearer ${token}`
        console.log("token is set")
    }
}
