using App.DTO.Identity;
using Base.Contracts.DAL;

namespace App.Contracts.Identity;

public interface IRefreshTokenRepository : IEntityRepository<RefreshToken>
{
    
}