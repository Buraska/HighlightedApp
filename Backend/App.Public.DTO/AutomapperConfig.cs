using App.Public.DTO.v1;
using App.Public.DTO.v1.Identity;
using AutoMapper;

namespace App.Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Book, Book>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, AppUser>().ReverseMap();
        CreateMap<BLL.DTO.Author, Author>().ReverseMap();
        CreateMap<BLL.DTO.Collect, Collect>().ReverseMap();
        CreateMap<BLL.DTO.FontFace, FontFace>().ReverseMap();
        CreateMap<BLL.DTO.Highlighted, Highlighted>().ReverseMap();
        CreateMap<BLL.DTO.HighlightedType, HighlightedType>().ReverseMap();
        CreateMap<BLL.DTO.Language, Language>().ReverseMap();
        CreateMap<BLL.DTO.Like, Like>().ReverseMap();
        CreateMap<BLL.DTO.Preference, Preference>().ReverseMap();
        CreateMap<BLL.DTO.Tag, Tag>().ReverseMap();
        CreateMap<BLL.DTO.BookCollect, BookCollect>().ReverseMap();
        CreateMap<BLL.DTO.BookTag, BookTag>().ReverseMap();
    }
}