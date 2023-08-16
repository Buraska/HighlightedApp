using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;

namespace App.DAL.EF.Repositories;

public class FontFaceRepository : BaseEntityRepository<FontFace, App.Domain.FontFace, AppDbContext>, IFontFaceRepository
{
    public FontFaceRepository(AppDbContext dbContext, IMapper<FontFace, Domain.FontFace> mapper) : base(dbContext, mapper)
    {
    }
}