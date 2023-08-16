using App.Contracts;
using App.DTO;
using Base.Contracts.Base;
using Base.DAL.EF;
using DAL.App;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class BookRepository : BaseEntityRepository<Book, App.Domain.Book, AppDbContext>, IBookRepository
{
    public BookRepository(AppDbContext dbContext, IMapper<Book, App.Domain.Book> mapper) : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<Book>> GetAllByNameAsync(string partialName, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.Title.ToUpper().Contains(partialName.ToUpper())).ToListAsync())
            .Select(x => Mapper.Map(x))!;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(b => b.Author)
            .Include(b => b.Language)
            .Include(u => u.AppUser)
            .Include(u => u.Preference).ThenInclude(x=> x!.FontFace)
            .Include(x => x.Tags)
            .Include(x => x.Highlighteds)
            .Include(x => x.Collects)
            .Where(m => m.AppUserId == userId);


        return (await query.ToListAsync()).Select(x=> Mapper.Map(x))!;
    }
}