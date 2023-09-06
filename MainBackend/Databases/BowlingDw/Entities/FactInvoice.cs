using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactInvoice : IEntity
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public float Amount { get; set; }

    public virtual DimClient DimClient { get; set; }
    public virtual DimDate DimDate { get; set; }
}