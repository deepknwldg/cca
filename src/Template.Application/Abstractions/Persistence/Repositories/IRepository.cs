using Template.Application.Models.Common;
using Template.Domain.ValueObjects;

namespace Template.Application.Abstractions.Persistence.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PagedResult<TEntity>> GetPagedAsync(PagingParams paging, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
