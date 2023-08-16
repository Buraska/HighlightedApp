using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class TagRepository : BaseEntityRepository<Tag, App.Domain.Tag, AppDbContext>, ITagRepository
{
    public TagRepository(AppDbContext dbContext, IMapper<Tag, Domain.Tag> mapper) : base(dbContext, mapper)
    {
    }
}