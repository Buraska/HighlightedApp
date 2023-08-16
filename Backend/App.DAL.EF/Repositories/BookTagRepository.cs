using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class BookTagRepository : BaseEntityRepository<BookTag, App.Domain.BookTag, AppDbContext>, IBookTagRepository
{
    public BookTagRepository(AppDbContext dbContext, IMapper<BookTag, Domain.BookTag> mapper) : base(dbContext, mapper)
    {
    }
}