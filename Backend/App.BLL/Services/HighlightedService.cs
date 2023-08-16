using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class HighlightedService : BaseEntityService<App.BLL.DTO.Highlighted, App.DTO.Highlighted, IHighlightedRepository>, IHighlightedService
{
    public HighlightedService(IHighlightedRepository repository, IMapper<Highlighted, App.DTO.Highlighted> mapper) : base(repository, mapper)
    {
    }
}