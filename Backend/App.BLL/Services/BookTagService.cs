using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class BookTagService : BaseEntityService<App.BLL.DTO.BookTag, App.DTO.BookTag, IBookTagRepository>, IBookTagService
{
    public BookTagService(IBookTagRepository repository, IMapper<BookTag, App.DTO.BookTag> mapper) : base(repository, mapper)
    {
    }
}