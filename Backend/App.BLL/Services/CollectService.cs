using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class CollectService : BaseEntityService<App.BLL.DTO.Collect, App.DTO.Collect, ICollectRepository>, ICollectService
{
    public CollectService(ICollectRepository repository, IMapper<Collect, App.DTO.Collect> mapper) : base(repository, mapper)
    {
    }
}