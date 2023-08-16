namespace Base.Contracts.BLL;

public interface IBLL
{
    public Task<int> SaveChangesAsync();

    public int SaveChanges();
    
}