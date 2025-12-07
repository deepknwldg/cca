using Mapster;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Users;
using Template.Domain.Entities;

namespace Template.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IUnitOfWork _uow;

    public UserService(IUserRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<UserResultDto> CreateAsync(CreateUserDto dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                Profile = new UserProfile
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                }
            };

            await _repo.AddAsync(user);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return user.Adapt<UserResultDto>();
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<UserResultDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetWithProfileAsync(id);
        return entity?.Adapt<UserResultDto>();
    }

    public async Task<IReadOnlyList<UserResultDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return users.Adapt<IReadOnlyList<UserResultDto>>();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var user = await _repo.GetWithProfileAsync(id);
            if (user == null) return false;

            user.Email = dto.Email;
            if (dto.PasswordHash != null)
                user.PasswordHash = dto.PasswordHash;

            user.Profile.FirstName = dto.FirstName;
            user.Profile.LastName = dto.LastName;

            _repo.Update(user);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return true;
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _uow.BeginTransactionAsync();
        try
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            _repo.Remove(user);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return true;
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }
}