using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IBookService : IEntityService<App.BLL.DTO.Book>
{
        
    Task<IEnumerable<Book>> GetAllByNameAsync(string partialName, bool noTracking = true);
    
    Task<IEnumerable<Book>> GetAllAsync(Guid userId, bool noTracking = true);
}