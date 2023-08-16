using App.DTO;
using App.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Book, App.Domain.Book>().ReverseMap();
        CreateMap<AppUser, App.Domain.Identity.AppUser>().ReverseMap();
        CreateMap<Author, App.Domain.Author>().ReverseMap();
        CreateMap<Collect, App.Domain.Collect>().ReverseMap();
        CreateMap<FontFace, App.Domain.FontFace>().ReverseMap();
        CreateMap<Highlighted, App.Domain.Highlighted>().ReverseMap();
        CreateMap<HighlightedType, App.Domain.HighlightedType>().ReverseMap();
        CreateMap<Language, App.Domain.Language>().ReverseMap();
        CreateMap<Like, App.Domain.Like>().ReverseMap();
        CreateMap<Preference, App.Domain.Preference>().ReverseMap();
        CreateMap<Tag, App.Domain.Tag>().ReverseMap();
        CreateMap<BookTag, App.Domain.BookTag>().ReverseMap();
        CreateMap<BookCollect, App.Domain.BookCollect>().ReverseMap();
    }
}