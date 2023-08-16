using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class TagService : BaseEntityService<App.BLL.DTO.Tag, App.DTO.Tag, ITagRepository>, ITagService
{
    public TagService(ITagRepository repository, IMapper<Tag, App.DTO.Tag> mapper) : base(repository, mapper)
    {
    }
}