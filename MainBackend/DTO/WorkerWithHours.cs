namespace MainBackend.DTO;

public class WorkerWithHours
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public double TotalWorkHours { get; set; }
}