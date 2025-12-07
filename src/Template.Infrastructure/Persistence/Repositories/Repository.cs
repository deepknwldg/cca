using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;

namespace Template.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id) =>
        await _db.Set<TEntity>().FindAsync(id);

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync() =>
        await _db.Set<TEntity>().ToListAsync();

    public virtual async Task AddAsync(TEntity entity) =>
        await _db.Set<TEntity>().AddAsync(entity);

    public virtual void Update(TEntity entity) =>
        _db.Set<TEntity>().Update(entity);

    public virtual void Remove(TEntity entity) =>
        _db.Set<TEntity>().Remove(entity);
}
