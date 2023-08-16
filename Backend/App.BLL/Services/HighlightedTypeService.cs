using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class HighlightedTypeService : BaseEntityService<App.BLL.DTO.HighlightedType, App.DTO.HighlightedType, IHighlightedTypeRepository>, IHighlightedTypeService
{
    public HighlightedTypeService(IHighlightedTypeRepository repository, IMapper<HighlightedType, App.DTO.HighlightedType> mapper) : base(repository, mapper)
    {
    }
}