using App.BLL.DTO;
using App.Contracts;
using App.Contracts.BLL.Services;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class BookService : BaseEntityService<App.BLL.DTO.Book, App.DTO.Book, IBookRepository>, IBookService
{
    public BookService(IBookRepository repository, IMapper<Book, App.DTO.Book> mapper) : base(repository, mapper)
    {
    }
    


    
    public async Task<IEnumerable<Book>> GetAllByNameAsync(string partialTitle, bool noTracking = true)
    {
        return (await Repository.GetAllByNameAsync(partialTitle, noTracking)).Select(x => Mapper.Map(x)!);
    }
    
    
    public async Task<IEnumerable<Book>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!).ToList();

        
        return res;
        
    }
}