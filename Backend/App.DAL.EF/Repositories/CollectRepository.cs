using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class CollectRepository : BaseEntityRepository<Collect, App.Domain.Collect, AppDbContext>, ICollectRepository
{
    public CollectRepository(AppDbContext dbContext, IMapper<Collect, Domain.Collect> mapper) : base(dbContext, mapper)
    {
    }
}