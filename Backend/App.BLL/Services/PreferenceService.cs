using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class PreferenceService : BaseEntityService<App.BLL.DTO.Preference, App.DTO.Preference, IPreferenceRepository>, IPreferenceService
{
    public PreferenceService(IPreferenceRepository repository, IMapper<Preference, App.DTO.Preference> mapper) : base(repository, mapper)
    {
    }
}