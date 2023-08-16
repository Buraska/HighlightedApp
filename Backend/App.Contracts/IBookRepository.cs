using App.DTO;
using Base.Contracts.DAL;

namespace App.Contracts;

public interface IBookRepository : IEntityRepository<Book>
{
    
    Task<IEnumerable<Book>> GetAllByNameAsync(string partialName, bool noTracking = true);
    
    Task<IEnumerable<Book>> GetAllAsync(Guid userId, bool noTracking = true);


}