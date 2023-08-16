using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class LikeService : BaseEntityService<App.BLL.DTO.Like, App.DTO.Like, ILikeRepository>, ILikeService
{
    public LikeService(ILikeRepository repository, IMapper<Like, App.DTO.Like> mapper) : base(repository, mapper)
    {
    }
}