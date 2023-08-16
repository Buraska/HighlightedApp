using App.DTO.Identity;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services.Identity;

public interface IAppUserService : IEntityService<App.BLL.DTO.Identity.AppUser>
{
     Task<ICollection<App.BLL.DTO.Identity.RefreshToken>> GetAllActiveTokens(Guid id, bool noTracking = true);

}