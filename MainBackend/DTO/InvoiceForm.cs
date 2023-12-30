namespace MainBackend.DTO;

public class InvoiceForm
{
    public DateTime PayingDate { get; set; }
    public int ClientUserId { get; set; }
    public ICollection<ProductForm> Products { get; set; }
    public class ProductForm
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}