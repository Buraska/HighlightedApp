using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class BookCollectService : BaseEntityService<App.BLL.DTO.BookCollect, App.DTO.BookCollect, IBookCollectRepository>, IBookCollectService
{
    public BookCollectService(IBookCollectRepository repository, IMapper<BookCollect, App.DTO.BookCollect> mapper) : base(repository, mapper)
    {
    }
}