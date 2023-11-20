import {useParams} from "react-router-dom";

export default function EditUser()
{
    const {id} = useParams()
    return(
        <>
            edit user {id}
        </>
    )
}