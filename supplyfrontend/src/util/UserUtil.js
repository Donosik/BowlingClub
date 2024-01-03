export function getIsLogged()
{
    if(localStorage.getItem('tokenSupply')!==null)
    {
        return true
    }
    return false
}