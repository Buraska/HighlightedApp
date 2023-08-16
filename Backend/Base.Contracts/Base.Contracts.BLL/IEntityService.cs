namespace Base.Contracts.BLL;

public interface IEntityService<TBllEntity> : IEntityService<TBllEntity, Guid>
{
}

public interface IEntityService<TBllEntity, TKey>
{

    public TBllEntity Add(TBllEntity entity);

    public TBllEntity Update(TBllEntity entity);

    public TBllEntity Remove(TBllEntity entity);

    public TBllEntity Remove(TKey id);

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true);
    public IEnumerable<TBllEntity> GetAll(bool noTracking = true);

    public bool Exists(TKey id);

    public  Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);

    public  Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true);

    public Task<bool> ExistsAsync(TKey id);

    public Task<TBllEntity> RemoveAsync(TKey id);

}