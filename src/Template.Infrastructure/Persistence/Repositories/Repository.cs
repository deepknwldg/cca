using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Models.Common;
using Template.Domain.ValueObjects;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Универсальный репозиторий, реализующий CRUD‑операции для любой
/// сущности домена, где <typeparamref name="TEntity"/> – класс сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности (должен быть классом).</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _db;

    /// <summary>
    /// Конструктор, получающий контекст через DI.
    /// </summary>
    /// <param name="db">Экземпляр <see cref="ApplicationDbContext"/>.</param>
    public Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().ToListAsync(cancellationToken);

    /// <inheritdoc />
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

    /// <inheritdoc />
    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default) =>
        await _db.Set<TEntity>().AddAsync(entity, cancellationToken);

    /// <inheritdoc />
    public virtual void Update(TEntity entity) =>
        _db.Set<TEntity>().Update(entity);

    /// <inheritdoc />
    public virtual void Remove(TEntity entity) =>
        _db.Set<TEntity>().Remove(entity);
}
