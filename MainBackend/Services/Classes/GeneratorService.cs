using System.Collections;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Classes;

public class GeneratorService : IGeneratorService
{
    private IClientService client { get; }
    private IUserService user { get; }
    private IWorkScheduleService workSchedule { get; }
    private IWorkerService worker { get; }
    private ILanesService lane { get; }
    private IReservationService reservation { get; }
    private IBarInventoryService barInventory { get; }
    private IInvoiceService invoice { get; }

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

    private readonly List<string> barItemsNames = new List<string>
    {
        "Napoje alkoholowe", "Piwo", "Wódka", "Whisky", "Koktajle", "Soki", "Napoje bezalkoholowe", "Woda gazowana",
        "Soki owocowe", "Limonada", "Przekąski", "Orzeszki", "Chipsy", "Nuggetsy", "Nachosy", "Owoce", "Cytryny",
        "Pomarańcze", "Limonki", "Dekoracje i dodatki", "Lód", "Słomki", "Parasolki do drinków", "Owoce do dekoracji",
        "Inne", "Kubki i szklanki", "Miksery", "Opakowania na napoje"
    };

    public GeneratorService(IUserService user, IWorkScheduleService workSchedule, IWorkerService worker,
        ILanesService lane, IReservationService reservation, IClientService client, IBarInventoryService barInventory,
        IInvoiceService invoice)
    {
        this.barInventory = barInventory;
        this.user = user;
        this.workSchedule = workSchedule;
        this.worker = worker;
        this.lane = lane;
        this.reservation = reservation;
        this.client = client;
        this.invoice = invoice;
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

            int isWorker = random.Next(20);
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
                    DateTime shiftStart = currentDate.Date.AddHours(14);
                    DateTime shiftEnd = currentDate.Date.AddHours(22);
                    await workSchedule.AddShift(worker, shiftStart, shiftEnd);
                }
                else
                {
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

    public async Task GenerateReservations(int normalDayReservations, int weekendReservations)
    {
        DateTime startDate = DateTime.Today;
        DateTime endDate = startDate.AddDays(30);
        IEnumerable<Client> clients = await client.GetClients();
        foreach (var currentDate in EachDay(startDate, endDate))
        {
            int reservationsToAdd =
                currentDate.DayOfWeek >= DayOfWeek.Monday && currentDate.DayOfWeek <= DayOfWeek.Friday
                    ? normalDayReservations
                    : weekendReservations;
            var selectedClients = clients.OrderBy(x => Guid.NewGuid()).Take(reservationsToAdd);

            foreach (var client in selectedClients)
            {
                if (currentDate.DayOfWeek >= DayOfWeek.Monday && currentDate.DayOfWeek <= DayOfWeek.Friday)
                {
                    // Dla dni roboczych - dodaj zmianę od 14:00 do 22:00
                    DateTime shiftStart = currentDate.Date.AddHours(14);
                    DateTime shiftEnd = currentDate.Date.AddHours(22);
                    await reservation.MakeReservation(shiftStart, shiftEnd, client);
                }
                else
                {
                    // Dla weekendów - dodaj zmianę od 10:00 do 22:00
                    DateTime shiftStart = currentDate.Date.AddHours(10);
                    DateTime shiftEnd = currentDate.Date.AddHours(22);
                    await reservation.MakeReservation(shiftStart, shiftEnd, client);
                }
            }
        }
    }

    public async Task GenerateBarInventories(int howManyItems)
    {
        for (int i = 0; i < howManyItems; i++)
        {
            String name = barItemsNames[random.Next(barItemsNames.Count)];
            int quantity = random.Next(1, 101);
            float price = (float)random.Next(100, 10000) / 100;
            await barInventory.AddBarItem(name, quantity, price);
        }
    }

    public async Task GenerateInvoices(int howManyInvoices)
    {
        ICollection<Client> clients = await client.GetClients();
        ICollection<BarInventory> barInventories = await barInventory.GetBarItems();
        for (int i = 0; i < howManyInvoices; i++)
        {
            Client client = clients.ElementAt(random.Next(clients.Count));
            ICollection<BarInventory> barItems = new List<BarInventory>();
            for (int j = 0; j < random.Next(5); i++)
            {
                barItems.Add(barInventories.ElementAt(random.Next(barInventories.Count)));
            }

            DateTime issueDate = DateTime.Today;
            issueDate = issueDate.AddDays(random.Next(15));
            DateTime dueDate = issueDate.AddDays(30);
            invoice.AddInvoice(barItems, client, issueDate, dueDate);
        }
    }

    public async Task GenerateAdmin()
    {
        RegisterForm registerForm = new RegisterForm();
        registerForm.FirstName = "Admin";
        registerForm.LastName = "Admin";
        registerForm.Login = "admin";
        registerForm.Password = "admin";
        registerForm.Email = "admin@admin.com";
        registerForm.DateOfBirth= new DateTime(2000, 1, 1);
        if(!await user.RegisterWorker(registerForm))
            return;
        var workers = await user.GetUsers();
        User worker = workers.FirstOrDefault(x => x.Login == registerForm.Login);
        if(worker == null)
            return;
        await user.ChangeToAdmin(worker.Person.Worker.Id, true);
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