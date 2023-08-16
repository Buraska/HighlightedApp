using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class LanguageService : BaseEntityService<App.BLL.DTO.Language, App.DTO.Language, ILanguageRepository>, ILanguageService
{
    public LanguageService(ILanguageRepository repository, IMapper<Language, App.DTO.Language> mapper) : base(repository, mapper)
    {
    }
}