using AutoMapper;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Abstractions.Services;
using Template.Application.Models.Courses;
using Template.Domain.Entities;

namespace Template.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        IUnitOfWork uow,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<CourseResultDto> CreateAsync(CreateCourseDto dto)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            var entity = _mapper.Map<Course>(dto);

            await _courseRepository.AddAsync(entity);
            await _uow.SaveChangesAsync();

            await _uow.CommitAsync();
            return _mapper.Map<CourseResultDto>(entity);
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }

    public async Task<CourseResultDto?> GetByIdAsync(Guid id)
    {
        var entity = await _courseRepository.GetByIdAsync(id);
        return _mapper.Map<CourseResultDto>(entity);
    }

    public async Task<IReadOnlyList<CourseResultDto>> GetAllAsync()
    {
        var items = await _courseRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CourseResultDto>>(items);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCourseDto dto)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            var entity = await _courseRepository.GetByIdAsync(id);
            if (entity is null)
            {
                await _uow.RollbackAsync();
                return false;
            }
            entity = _mapper.Map<Course>(dto);
            _courseRepository.Update(entity);

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
            var entity = await _courseRepository.GetByIdAsync(id);
            if (entity is null)
            {
                await _uow.RollbackAsync();
                return false;
            }

            _courseRepository.Remove(entity);

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
