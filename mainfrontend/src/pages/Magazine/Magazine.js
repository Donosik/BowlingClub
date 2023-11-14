import MagazineTable from "./MagazineTable";
import "./Magazine.css"

export default function Magazine()
{
    const products=[
        {
            Id:1,
            Name:"Kula do kręgli",
        },
    ]
    return(
        <>
           
            <MagazineTable products={products}/>
        </>
    )
}