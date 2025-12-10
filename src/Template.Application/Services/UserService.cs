using AutoMapper;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Common;
using Template.Application.Models.Users;
using Template.Domain.Entities;
using Template.Domain.ValueObjects;

namespace Template.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository repo,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _repo = repo;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<UserResultDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = dto.Password,
                Profile = new UserProfile
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                }
            };

            await _repo.AddAsync(user, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<UserResultDto>(user);
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<UserResultDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repo.GetWithProfileAsync(id, cancellationToken);
        return _mapper.Map<UserResultDto>(entity);
    }

    public async Task<PagedResult<UserResultDto>> GetAllAsync(PagingParams paging, CancellationToken cancellationToken = default)
    {
        var pagedList = await _repo.GetPagedAsync(paging, cancellationToken);
        return _mapper.Map<PagedResult<UserResultDto>>(pagedList);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await _repo.GetWithProfileAsync(id, cancellationToken);
            if (user == null)
            {
                return false;
            }

            user.Email = dto.Email;
            if (dto.PasswordHash != null)
            {
                user.PasswordHash = dto.PasswordHash;
            }

            user.Profile.FirstName = dto.FirstName;
            user.Profile.LastName = dto.LastName;

            _repo.Update(user);
            await _uow.SaveChangesAsync(cancellationToken);

            await _uow.CommitAsync(cancellationToken);
            return true;
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _uow.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await _repo.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return false;
            }

            _repo.Remove(user);
            await _uow.SaveChangesAsync(cancellationToken);

            await _uow.CommitAsync(cancellationToken);
            return true;
        }
        catch
        {
            await _uow.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
