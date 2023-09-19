namespace MainBackend.DTO;

public class InvoicesWithProducts
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int TotalSold { get; set; }
}