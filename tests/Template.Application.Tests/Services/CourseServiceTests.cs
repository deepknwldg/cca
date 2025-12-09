using Mapster;
using NSubstitute;
using Shouldly;
using Template.Application.Abstractions.MapsterMapper;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Models.Courses;
using Template.Application.Services;
using Template.Domain.Entities;

namespace Template.Application.Tests.Services;

public class CourseServiceTests
{
    private readonly ICourseRepository _repo = Substitute.For<ICourseRepository>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
    private readonly IObjectMapper _mapper = Substitute.For<IObjectMapper>();

    // System Under Test
    private readonly CourseService _sut;

    public CourseServiceTests()
    {
        _sut = new CourseService(_repo, _uow, _mapper);
    }

    #region CreateAsync
    [Fact]
    public async Task CreateAsync_Should_Map_Entity_And_Return_Result()
    {
        // Arrange
        var dto = new CreateCourseDto { Title = "ASP.NET Core", Description = "Web API" };
        var entity = new Course { Id = Guid.NewGuid(), Title = dto.Title, Description = dto.Description };
        var resultDto = new CourseResultDto(entity.Id, entity.Title, entity.Description);

        _mapper.Adapt<CreateCourseDto, Course>(dto).Returns(entity);
        _mapper.Adapt<Course, CourseResultDto>(entity).Returns(resultDto);

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
        await _repo.Received(1).AddAsync(entity);
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
        result.ShouldBe(resultDto);
    }

    [Fact]
    public async Task CreateAsync_When_Exception_Thrown_Should_Rollback_And_ReThrow()
    {
        // Arrange
        var dto = new CreateCourseDto { Title = "Bad", Description = null };
        var entity = new Course { Id = Guid.NewGuid(), Title = "Bad", Description = null };
        var ex = new InvalidOperationException("boom");

        _mapper.Adapt<CreateCourseDto, Course>(dto).Returns(entity);
        _repo.When(r => r.AddAsync(entity)).Do(_ => throw ex);

        // Act
        var thrown = await Should.ThrowAsync<InvalidOperationException>(() => _sut.CreateAsync(dto));

        // Assert
        thrown.ShouldBeSameAs(ex);
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    public async Task GetByIdAsync_When_Found_Returns_Dto()
    {
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "C#", Description = "dotnet" };
        var dto = new CourseResultDto(id, entity.Title, entity.Description);

        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));
        _mapper.Adapt<Course, CourseResultDto>(entity).Returns(dto);

        var result = await _sut.GetByIdAsync(id);

        result.ShouldNotBeNull();
        result!.ShouldBe(dto);
    }

    [Fact]
    public async Task GetByIdAsync_When_NotFound_Returns_Null()
    {
        var id = Guid.NewGuid();
        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        var result = await _sut.GetByIdAsync(id);
        result.ShouldBeNull();
    }

    #endregion

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_Returns_Mapped_List()
    {
        var courses = new List<Course>
            {
                new() { Id = Guid.NewGuid(), Title = "A", Description = "a" },
                new() { Id = Guid.NewGuid(), Title = "B", Description = "b" }
            };
        var dtos = new List<CourseResultDto>
            {
                new(courses[0].Id, courses[0].Title, courses[0].Description),
                new(courses[1].Id, courses[1].Title, courses[1].Description)
            };

        _repo.GetAllAsync().Returns(Task.FromResult<IReadOnlyList<Course>>(courses));
        _mapper.Adapt<IReadOnlyList<Course>, IReadOnlyList<CourseResultDto>>(courses)
            .Returns(dtos);

        var result = await _sut.GetAllAsync();

        result.ShouldBe(dtos);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_When_Found_Updates_And_Returns_True()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existing = new Course
        {
            Id = id,
            Title = "Old title",
            Description = "Old description"
        };
        var dto = new UpdateCourseDto
        {
            Title = "New title",
            Description = "New description"
        };

        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(existing));
        _mapper.Adapt<UpdateCourseDto, Course>(dto, Arg.Any<Course>());            

        // Act
        var result = await _sut.UpdateAsync(id, dto);

        // Assert
        result.ShouldBeTrue();

        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());

        // Проверяем, что объект, который попал в репозиторий, уже имеет новые значения
        _repo.Received(1).Update(
            Arg.Is<Course>(c =>
                c.Id == id &&                     // Id НЕ изменился
                c.Title == dto.Title &&           // Title изменился
                c.Description == dto.Description  // Description изменился
            ));

        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_When_NotFound_RollsBack_And_Returns_False()
    {
        var id = Guid.NewGuid();
        var dto = new UpdateCourseDto { Title = "X", Description = "Y" };
        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        var result = await _sut.UpdateAsync(id, dto);

        result.ShouldBeFalse();
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
        _repo.DidNotReceive().Update(Arg.Any<Course>());
    }

    [Fact]
    public async Task UpdateAsync_When_Exception_RollsBack_And_ReThrows()
    {
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "A", Description = "B" };
        var dto = new UpdateCourseDto { Title = "C", Description = "D" };
#pragma warning disable CA2201 // Не порождайте исключения зарезервированных типов
        var ex = new Exception("boom");
#pragma warning restore CA2201 // Не порождайте исключения зарезервированных типов

        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));
        _uow.When(u => u.SaveChangesAsync(Arg.Any<CancellationToken>())).Do(_ => throw ex);

        var thrown = await Should.ThrowAsync<Exception>(() => _sut.UpdateAsync(id, dto));
        thrown.ShouldBeSameAs(ex);
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_When_Found_Deletes_And_Returns_True()
    {
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "Del", Description = "Del" };
        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));

        var result = await _sut.DeleteAsync(id);

        result.ShouldBeTrue();
        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
        _repo.Received(1).Remove(entity);
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_When_NotFound_RollsBack_And_Returns_False()
    {
        var id = Guid.NewGuid();
        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        var result = await _sut.DeleteAsync(id);

        result.ShouldBeFalse();
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
        _repo.DidNotReceive().Remove(Arg.Any<Course>());
    }

    [Fact]
    public async Task DeleteAsync_When_Exception_RollsBack_And_ReThrows()
    {
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "X", Description = "Y" };
        var ex = new InvalidOperationException("boom");

        _repo.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));
        _uow.When(u => u.SaveChangesAsync(Arg.Any<CancellationToken>())).Do(_ => throw ex);

        var thrown = await Should.ThrowAsync<InvalidOperationException>(() => _sut.DeleteAsync(id));
        thrown.ShouldBeSameAs(ex);
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion
}
