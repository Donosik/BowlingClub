using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class PromotionService : IPromotionService
{
    private readonly IRepositoryWrapper repositoryWrapper;

    public PromotionService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<Promotion>> GetPromotions()
    {
        ICollection<Promotion> promotions = await repositoryWrapper.normalDbWrapper.promotion.GetAll();
        if (promotions.Count == 7)
            promotions = await CheckDuplicates(promotions);
        if (promotions.Count == 7)
            return promotions;
        throw new Exception("Promotions count in database is " + promotions.Count);
    }

    public async Task<bool> CreateDefaultPromotions()
    {
        await DeletePromotions();
        var defaultPromotions = new List<Promotion>();
        for (int i = 1; i <= 7; i++)
        {
            var promotion = new Promotion
            {
                dayOfWeek = (DayOfWeek)(i % 7),
                description = ""
            };

            defaultPromotions.Add(promotion);
        }

        foreach (var promotion in defaultPromotions)
        {
            repositoryWrapper.normalDbWrapper.promotion.Create(promotion);
        }

        return await repositoryWrapper.normalDbWrapper.Save(defaultPromotions.Count);
    }

    public async Task<bool> ChangePromotions(ICollection<Promotion> promotions)
    {
        var currentPromotions = await GetPromotions();
        int updatedEntities = 0;
        foreach (var promotion in promotions)
        {
            var existingPromotion = currentPromotions.FirstOrDefault(x => x.dayOfWeek == promotion.dayOfWeek);

            if (existingPromotion != null)
            {
                existingPromotion.description = promotion.description;
                repositoryWrapper.normalDbWrapper.promotion.Edit(existingPromotion);
                updatedEntities++;
            }
        }

        return await repositoryWrapper.normalDbWrapper.Save(updatedEntities);
    }

    public async Task<bool> DeletePromotions()
    {
        var promotions = await repositoryWrapper.normalDbWrapper.promotion.GetAll();
        int count = promotions.Count;
        foreach (var promotion in promotions)
        {
            repositoryWrapper.normalDbWrapper.promotion.Delete(promotion);
        }

        return await repositoryWrapper.normalDbWrapper.Save(count);
    }

    private async Task<ICollection<Promotion>> CheckDuplicates(ICollection<Promotion> promotions)
    {
        var uniqueDays = new HashSet<DayOfWeek>();
        var uniquePromotions = new List<Promotion>();

        foreach (var promotion in promotions)
        {
            if (uniqueDays.Add(promotion.dayOfWeek))
            {
                uniquePromotions.Add(promotion);
            }
        }

        return uniquePromotions;
    }
}