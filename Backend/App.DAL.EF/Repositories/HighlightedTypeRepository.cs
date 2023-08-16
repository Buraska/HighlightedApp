using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class HighlightedTypeRepository : BaseEntityRepository<HighlightedType,App.Domain.HighlightedType, AppDbContext>, IHighlightedTypeRepository
{
    public HighlightedTypeRepository(AppDbContext dbContext, IMapper<HighlightedType, Domain.HighlightedType> mapper) : base(dbContext, mapper)
    {
    }
}