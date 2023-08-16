using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class PreferenceRepository : BaseEntityRepository<Preference, App.Domain.Preference, AppDbContext>, IPreferenceRepository
{
    public PreferenceRepository(AppDbContext dbContext, IMapper<Preference, Domain.Preference> mapper) : base(dbContext, mapper)
    {
    }
}