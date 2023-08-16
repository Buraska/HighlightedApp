using App.BLL.DTO.Identity;
using App.Contracts;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.Identity;
using Base.BLL;
using Base.Contracts.Base;
using RefreshToken = App.BLL.DTO.Identity.RefreshToken;

namespace App.BLL.Services.Identity;

public class AppUserService :  BaseEntityService<App.BLL.DTO.Identity.AppUser, App.DTO.Identity.AppUser, IAppUserRepository>, IAppUserService
{
    public AppUserService(IAppUserRepository repository, IMapper<AppUser, App.DTO.Identity.AppUser> mapper) : base(repository, mapper)
    {
    }

    public async Task<ICollection<RefreshToken>> GetAllActiveTokens(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(Repository.FirstOrDefault(id))!.RefreshTokens.Where(x =>
            (x.TokenExpirationDateTime > DateTime.UtcNow) ||
            (x.PreviousTokenExpirationDateTime > DateTime.UtcNow)
        );
        return res.ToList();
    }
}