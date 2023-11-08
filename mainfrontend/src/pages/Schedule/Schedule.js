import WorkerSchedule from "./WorkerSchedule";

export default function Schedule()
{
    const workersSchedule = [{
        Id: 1,
        FirstName: "John",
        LastName: "Doe",
        Availability: [
            {
                start: "1990-01-01",
                end: "1990-01-01"
            },
            {
                start: "1990-01-01",
                end: "1990-01-01"
            },
        ],
    },{
        Id: 1,
        FirstName: "John",
        LastName: "Doe",
        Availability: [
            {
                start: "1990-01-01",
                end: "1990-01-01"
            },
            {
                start: "1990-01-01",
                end: "1990-01-01"
            },
        ],
    },
    ]

    return (
        <>
            Grafik
            <WorkerSchedule workersSchedule={workersSchedule}/>
        </>
    )
}