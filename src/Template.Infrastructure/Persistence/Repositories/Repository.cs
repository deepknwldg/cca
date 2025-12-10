using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Models.Common;
using Template.Domain.ValueObjects;

namespace Template.Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().FindAsync([id], cancellationToken);

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().ToListAsync(cancellationToken);

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        PagingParams paging,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Set<TEntity>().AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(paging.Skip)
            .Take(paging.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>
        {
            Items = items,
            PageNumber = paging.PageNumber,
            PageSize = paging.PageSize,
            TotalCount = totalCount
        };
    }

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().AddAsync(entity, cancellationToken);

    public virtual void Update(TEntity entity) =>
        _db.Set<TEntity>().Update(entity);

    public virtual void Remove(TEntity entity) =>
        _db.Set<TEntity>().Remove(entity);
}
