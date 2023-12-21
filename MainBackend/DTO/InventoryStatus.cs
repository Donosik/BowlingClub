namespace MainBackend.DTO;

public class InventoryStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TargetQuantity { get; set; }
    public int CurrentQuantity { get; set; }
    public decimal Price { get; set; }
}