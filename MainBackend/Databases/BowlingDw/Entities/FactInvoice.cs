using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactInvoice : IEntity
{
    public int Id { get; set; }
    public float Amount { get; set; }

    public virtual DimClient Client { get; set; }
    public virtual int IssueDateId { get; set; }
    public virtual DimDate IssueDate { get; set; }
    public virtual int DueDateId { get; set; }
    public virtual DimDate DueDate { get; set; }
    public virtual ICollection<DimProduct> Products { get; set; }
}