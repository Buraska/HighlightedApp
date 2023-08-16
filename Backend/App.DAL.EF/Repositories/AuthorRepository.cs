using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class AuthorRepository : BaseEntityRepository<Author, App.Domain.Author, AppDbContext>, IAuthorRepository
{
    public AuthorRepository(AppDbContext dbContext, IMapper<Author, Domain.Author> mapper) : base(dbContext, mapper)
    {
    }
}