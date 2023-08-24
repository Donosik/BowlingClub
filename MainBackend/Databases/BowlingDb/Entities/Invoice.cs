using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Invoice : IEntity
{
    public int id { get; set; }

    public DateTime issueDate { get; set; }
    public DateTime dueDate { get; set; }
    public float amount { get; set; }
    
    public virtual Client client { get; set; }
    //public virtual Worker worker { get; set; }
}