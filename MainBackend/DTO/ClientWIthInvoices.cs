namespace MainBackend.DTO;

public class ClientWIthInvoices
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int TotalInvoices { get; set; }
}