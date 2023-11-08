import MagazineTable from "./MagazineTable";

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
            Magazyn
            <MagazineTable products={products}/>
        </>
    )
}