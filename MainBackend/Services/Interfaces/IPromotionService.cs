using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IPromotionService
{
    Task<ICollection<Promotion>> GetPromotions();
    Task<bool> CreateDefaultPromotions();
    Task<bool> ChangePromotions(ICollection<Promotion> promotions);
    Task<bool> DeletePromotions();
}