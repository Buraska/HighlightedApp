using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class BookCollectRepository : BaseEntityRepository<BookCollect, App.Domain.BookCollect, AppDbContext>, IBookCollectRepository
{
    public BookCollectRepository(AppDbContext dbContext, IMapper<BookCollect, Domain.BookCollect> mapper) : base(dbContext, mapper)
    {
    }
}