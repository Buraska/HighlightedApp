using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class FontFaceService : BaseEntityService<App.BLL.DTO.FontFace, App.DTO.FontFace, IFontFaceRepository>, IFontFaceService
{
    public FontFaceService(IFontFaceRepository repository, IMapper<FontFace, App.DTO.FontFace> mapper) : base(repository, mapper)
    {
    }
}