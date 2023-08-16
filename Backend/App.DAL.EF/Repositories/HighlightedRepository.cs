using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class HighlightedRepository : BaseEntityRepository<Highlighted, App.Domain.Highlighted, AppDbContext>, IHighlightedRepository
{
    public HighlightedRepository(AppDbContext dbContext, IMapper<Highlighted, Domain.Highlighted> mapper) : base(dbContext, mapper)
    {
    }
    
    public virtual async Task<IEnumerable<Highlighted>> GetAllAsync(bool noTracking = true)
    {
        var highs = await CreateQuery(noTracking).ToListAsync();

        foreach (var h in highs)
        {
            h.HighlightedType = await RepoDbContext.HighlightedTypes.FirstOrDefaultAsync(x => x.Id == h.HighlightedTypeId);
        }
        
        return highs.Select(x => Mapper.Map(x)!);
    }
    public virtual IEnumerable<Highlighted> GetAll(bool noTracking = true)
    {
        var highs = CreateQuery(noTracking).ToList();

        foreach (var h in highs)
        {
            h.HighlightedType = RepoDbContext.HighlightedTypes.FirstOrDefault(x => x.Id == h.HighlightedTypeId);
        }
        
        return highs.Select(x => Mapper.Map(x)!);
    }
    
    /*public virtual Highlighted? FirstOrDefault(Highlighted id, bool noTracking = true)
    {
        return
            Mapper.Map(
                CreateQuery(noTracking)
                    .FirstOrDefault(a => a.Id.Equals(id))
            );
    }*/
    
}