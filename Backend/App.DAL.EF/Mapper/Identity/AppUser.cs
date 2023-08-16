using App.DTO.Identity;
using AutoMapper;
using Base.Contracts;
using Base.Contracts.Base;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class AppUserMapper : BaseMapper<AppUser, App.Domain.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}