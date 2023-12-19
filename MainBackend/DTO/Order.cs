namespace MainBackend.DTO;

public class Order
{
    public int Id { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public bool IsFulfilled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}