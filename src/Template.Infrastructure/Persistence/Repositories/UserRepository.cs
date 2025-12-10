using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Models.Common;
using Template.Domain.Entities;
using Template.Domain.ValueObjects;

namespace Template.Infrastructure.Persistence.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью <see cref="User"/>.  
/// Наследуется от базового <see cref="Repository{User}"/> и реализует
/// методы, требующие eager‑loading профиля и пагинации с профилем.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    /// <summary>
    /// Инициализирует репозиторий, получая контекст БД.
    /// </summary>
    /// <param name="db">Экземпляр <see cref="ApplicationDbContext"/>.</param>
    public UserRepository(ApplicationDbContext db) : base(db) { }

    /// <inheritdoc />
    public Task<User?> GetWithProfileAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public override async Task<PagedResult<User>> GetPagedAsync(
         PagingParams paging,
         CancellationToken cancellationToken = default)
    {
        var query = _db.Users
            .Include(u => u.Profile)
            .AsNoTracking()
            .OrderBy(u => u.Id);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(paging.Skip)
            .Take(paging.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<User>
        {
            Items = items,
            PageNumber = paging.PageNumber,
            PageSize = paging.PageSize,
            TotalCount = totalCount
        };
    }
}
