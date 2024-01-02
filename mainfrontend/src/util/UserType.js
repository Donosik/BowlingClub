export function getIsWorker()
{
    if (localStorage.getItem('token') !== null)
    {
        const token = localStorage.getItem('token')
        const claims=parseJwt(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        if(claims[2]&&claims[2]==="Admin")
        {
            return true
        }
        else if(claims[1]&&claims[1]==="Worker")
        {
            return false
        }
        else {
            return false
        }
    }
    return true
}

export function getIsAdmin()
{
    if (localStorage.getItem('token') !== null)
    {
        const token = localStorage.getItem('token')
        const claims=parseJwt(token)["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        if(claims[2]==="Admin")
        {
            return false
        }
        else if(claims[1]==="Worker")
        {
            return false
        }
        else {
            return false
        }
    }
    return true
}

export function isUserLoggedIn()
{
    if (localStorage.getItem('token') !== null)
    {
        return true
    }
    return false
}

function parseJwt (token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}