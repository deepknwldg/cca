using Template.Domain.Entities;

namespace Template.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetWithProfileAsync(Guid id, CancellationToken cancellationToken = default);
}
