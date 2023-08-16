using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class AuthorService : BaseEntityService<App.BLL.DTO.Author, App.DTO.Author, IAuthorRepository>, IAuthorService
{
    public AuthorService(IAuthorRepository repository, IMapper<Author, App.DTO.Author> mapper) : base(repository, mapper)
    {
    }
    
}