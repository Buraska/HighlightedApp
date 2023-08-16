using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class LanguageRepository : BaseEntityRepository<Language, App.Domain.Language, AppDbContext>, ILanguageRepository
{
    public LanguageRepository(AppDbContext dbContext, IMapper<Language, Domain.Language> mapper) : base(dbContext, mapper)
    {
    }
}