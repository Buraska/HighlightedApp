using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.Identity;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services.Identity;

public class RefreshTokenService : BaseEntityService<App.BLL.DTO.Identity.RefreshToken, App.DTO.Identity.RefreshToken, IRefreshTokenRepository>, IRefreshTokenService
{
    public RefreshTokenService(IRefreshTokenRepository repository, IMapper<RefreshToken, App.DTO.Identity.RefreshToken> mapper) : base(repository, mapper)
    {
    }
}