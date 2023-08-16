using App.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mapper;

public class BookMapper : BaseMapper<DTO.Book, Domain.Book>
{
    public BookMapper(IMapper mapper) : base(mapper)
    {
    }
}