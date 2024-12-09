using ODataMemoryLeak.Services.Entities;

namespace ODataMemoryLeak.Services;

public interface IDataService
{
    IQueryable<T> Get<T>() where T : class;
}

public class DataService : IDataService
{
    private readonly AppDbContext _db;
    public DataService(AppDbContext db)
    {
        _db = db;
    }

    public IQueryable<T> Get<T>() where T : class
    {
        if (typeof(T) == typeof(HumanEntity))
        {
            return _db.Set<HumanEntity>().Cast<T>().AsQueryable();

        }

        if (typeof(T) == typeof(HumanTableEntity))
        {
            return _db.Set<HumanTableEntity>().Cast<T>().AsQueryable();
        }

        return _db.Set<T>().Cast<T>().AsQueryable();
    }

    public void Add<TEntity>(TEntity entity) where TEntity : class, new()
    {
        _db.Set<TEntity>().Add(entity);
        _db.SaveChanges();
    }
}