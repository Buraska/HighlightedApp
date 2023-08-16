using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Book, App.DTO.Book>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DTO.Identity.AppUser>().ReverseMap();
        
        CreateMap<DTO.Author, App.DTO.Author>().ReverseMap();
        CreateMap<DTO.Collect, App.DTO.Collect>().ReverseMap();
        CreateMap<DTO.FontFace, App.DTO.FontFace>().ReverseMap();
        CreateMap<DTO.Highlighted, App.DTO.Highlighted>().ReverseMap();
        CreateMap<DTO.HighlightedType, App.DTO.HighlightedType>().ReverseMap();
        CreateMap<DTO.Language, App.DTO.Language>().ReverseMap();
        CreateMap<DTO.Like, App.DTO.Like>().ReverseMap();
        CreateMap<DTO.Preference, App.DTO.Preference>().ReverseMap();
        CreateMap<DTO.Tag, App.DTO.Tag>().ReverseMap();
        CreateMap<DTO.BookCollect, App.DTO.BookCollect>().ReverseMap();
        CreateMap<DTO.BookTag, App.DTO.BookTag>().ReverseMap();
    }
}