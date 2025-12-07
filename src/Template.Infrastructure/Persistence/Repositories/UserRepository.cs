using Microsoft.EntityFrameworkCore;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext db) : base(db) { }

    public Task<User?> GetWithProfileAsync(Guid id)
    {
        return _db.Users
            .Include(x => x.Profile)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
