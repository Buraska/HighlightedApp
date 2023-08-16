using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class LikeRepository : BaseEntityRepository<Like, App.Domain.Like, AppDbContext>, ILikeRepository
{
    public LikeRepository(AppDbContext dbContext, IMapper<Like, Domain.Like> mapper) : base(dbContext, mapper)
    {
    }
}