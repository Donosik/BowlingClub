using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Classes;

public class GeneratorService : IGeneratorService
{
    private IUserService user { get; }
    private IWorkScheduleService workSchedule { get; }
    private IWorkerService worker { get; }
    private ILanesService lane { get; }
    private IReservationService reservation { get; }

    private readonly Random random = new Random();

    private readonly List<string> firstNames = new List<string>
    {
        "Anna", "Marek", "Katarzyna", "Piotr", "Magdalena", "Krzysztof", "Ewa", "Tomasz", "Agnieszka", "Jan",
        "Karolina", "Marcin", "Joanna", "Łukasz", "Natalia", "Michał", "Beata", "Adam", "Izabela", "Kamil"
    };

    private readonly List<string> lastNames = new List<string>
    {
        "Nowak", "Kowalski", "Wiśniewski", "Dąbrowski", "Lewandowski", "Wójcik", "Kamiński", "Kowalczyk", "Zieliński",
        "Szymański",
        "Jankowski", "Wojciechowski", "Kowal", "Witkowski", "Walczak", "Stępień", "Dudek", "Pawlak", "Kowalczyk",
        "Gajewski"
    };

    public GeneratorService(IUserService user, IWorkScheduleService workSchedule, IWorkerService worker,
        ILanesService lane,IReservationService reservation)
    {
        this.user = user;
        this.workSchedule = workSchedule;
        this.worker = worker;
        this.lane = lane;
        this.reservation = reservation;
    }

    public async Task GenerateUsers(int howManyUsersToGenerate)
    {
        for (int i = 0; i < howManyUsersToGenerate; i++)
        {
            RegisterForm registerForm = GenerateRandomUser();
            int isClient = random.Next(2);
            if (isClient == 1)
            {
                await user.RegisterClient(registerForm);
            }

            int isWorker = random.Next(100);
            if (isWorker == 0)
            {
                registerForm.Login = GenerateRandomLogin(registerForm.FirstName, registerForm.LastName);
                await user.RegisterWorker(registerForm);
            }
        }
    }

    public async Task GenerateShifts(int normalDayShifts, int weekendShifts)
    {
        DateTime startDate = DateTime.Today;
        DateTime endDate = startDate.AddDays(30);
        IEnumerable<Worker> workers = await worker.GetWorkers();

        foreach (var currentDate in EachDay(startDate, endDate))
        {
            int shiftsToAdd = currentDate.DayOfWeek >= DayOfWeek.Monday && currentDate.DayOfWeek <= DayOfWeek.Friday
                ? normalDayShifts
                : weekendShifts;
            var selectedWorkers = workers.OrderBy(x => Guid.NewGuid()).Take(shiftsToAdd);

            foreach (var worker in selectedWorkers)
            {
                if (currentDate.DayOfWeek >= DayOfWeek.Monday && currentDate.DayOfWeek <= DayOfWeek.Friday)
                {
                    // Dla dni roboczych - dodaj zmianę od 14:00 do 22:00
                    DateTime shiftStart = currentDate.Date.AddHours(14);
                    DateTime shiftEnd = currentDate.Date.AddHours(22);
                    await workSchedule.AddShift(worker, shiftStart, shiftEnd);
                }
                else
                {
                    // Dla weekendów - dodaj zmianę od 10:00 do 22:00
                    DateTime shiftStart = currentDate.Date.AddHours(10);
                    DateTime shiftEnd = currentDate.Date.AddHours(22);
                    await workSchedule.AddShift(worker, shiftStart, shiftEnd);
                }
            }
        }
    }

    public async Task GenerateLanes(int howManyLanes)
    {
        for (int i = 0; i < howManyLanes; i++)
        {
            await lane.AddLane(i);
        }
    }

    public async Task GenerateReservations(int howManyReservations)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
    {
        for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
        {
            yield return day;
        }
    }

    private RegisterForm GenerateRandomUser()
    {
        RegisterForm registerForm = new RegisterForm();
        string firstName = firstNames[random.Next(firstNames.Count)];
        string lastName = lastNames[random.Next(lastNames.Count)];
        registerForm.FirstName = firstName;
        registerForm.LastName = lastName;
        registerForm.Login = GenerateRandomLogin(firstName, lastName);
        registerForm.Password = GenerateRandomPassword(firstName, lastName);
        registerForm.Email = GenerateRandomEmail(firstName, lastName);
        registerForm.DateOfBirth = GenerateRandomDateOfBirth();
        return registerForm;
    }

    private string GenerateRandomLogin(string firstName, string lastName)
    {
        return $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(100)}";
    }

    private string GenerateRandomPassword(string firstName, string lastName)
    {
        return $"{firstName.ToLower().Substring(0, 3)}{lastName.ToLower().Substring(0, 3)}{random.Next(1000)}";
    }

    private string GenerateRandomEmail(string firstName, string lastName)
    {
        return $"{firstName.ToLower()}.{lastName.ToLower()}{random.Next(100)}@example.com";
    }

    private DateTime GenerateRandomDateOfBirth()
    {
        DateTime minDate = new DateTime(1950, 1, 1);
        DateTime maxDate = new DateTime(2005, 12, 31);
        DateTime randomDate = minDate.AddDays(random.Next((maxDate - minDate).Days));
        return randomDate;
    }
}