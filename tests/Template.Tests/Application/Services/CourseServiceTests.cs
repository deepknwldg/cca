using Mapster;
using NSubstitute;
using Shouldly;
using Template.Api.Mapping;
using Template.Application.Abstractions.Persistence.Repositories;
using Template.Application.Models.Courses;
using Template.Application.Services;
using Template.Domain.Entities;

namespace Template.Tests.Application.Services;

public class CourseServiceTests
{
    private readonly ICourseRepository _courseRepository = Substitute.For<ICourseRepository>();
    private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

    private readonly CourseService _sut;

    public CourseServiceTests()
    {
        var config = new TypeAdapterConfig();
        new ApiMappingProfile().Register(config);

        _sut = new CourseService(_courseRepository, _uow);
    }

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_Should_Create_Entity_And_Return_Result()
    {
        // Arrange
        var dto = new CreateCourseDto
        {
            Title = "Math 101",
            Description = "Базовый курс математики"
        };

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
        await _courseRepository.Received(1).AddAsync(Arg.Is<Course>(c =>
            c.Title == dto.Title && c.Description == dto.Description));
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());

        result.ShouldNotBeNull();
        result.Title.ShouldBe(dto.Title);
        result.Description.ShouldBe(dto.Description);
    }

    [Fact]
    public async Task CreateAsync_When_Exception_Thrown_Should_Rollback_And_ReThrow()
    {
        // Arrange
        var dto = new CreateCourseDto { Title = "Physics", Description = null };
        var expected = new InvalidOperationException("boom");

        // Force repository to throw
        _courseRepository
            .When(r => r.AddAsync(Arg.Any<Course>()))
            .Do(_ => throw expected);

        // Act & Assert
        var ex = await Should.ThrowAsync<InvalidOperationException>(() => _sut.CreateAsync(dto));
        ex.ShouldBeSameAs(expected);

        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    public async Task GetByIdAsync_When_Entity_Exists_Should_Return_Dto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Course
        {
            Id = id,
            Title = "Chemistry",
            Description = "Organic chemistry"
        };
        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));

        // Act
        var result = await _sut.GetByIdAsync(id);

        // Assert
        result.ShouldNotBeNull();
        result!.Id.ShouldBe(id);
        result.Title.ShouldBe(entity.Title);
        result.Description.ShouldBe(entity.Description);
    }

    [Fact]
    public async Task GetByIdAsync_When_Not_Found_Should_Return_Null()
    {
        // Arrange
        var id = Guid.NewGuid();
        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        // Act
        var result = await _sut.GetByIdAsync(id);

        // Assert
        result.ShouldBeNull();
    }

    #endregion

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_Should_Return_Mapped_List()
    {
        // Arrange
        var entities = new List<Course>
        {
            new Course { Id = Guid.NewGuid(), Title = "History", Description = "World history" },
            new Course { Id = Guid.NewGuid(), Title = "Biology", Description = "Human anatomy" }
        };
        _courseRepository.GetAllAsync().Returns(Task.FromResult<IReadOnlyList<Course>>(entities));

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(entities[0].Id);
        result[0].Title.ShouldBe(entities[0].Title);
        result[0].Description.ShouldBe(entities[0].Description);
    }

    [Fact]
    public async Task GetAllAsync_When_Empty_Should_Return_Empty_List()
    {
        // Arrange
        _courseRepository.GetAllAsync().Returns(Task.FromResult<IReadOnlyList<Course>>(Array.Empty<Course>()));

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.ShouldBeEmpty();
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_When_Entity_Exists_Should_Update_And_Return_True()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existing = new Course { Id = id, Title = "Old", Description = "Old desc" };
        var dto = new UpdateCourseDto { Title = "New", Description = "New desc" };

        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(existing));

        // Act
        var result = await _sut.UpdateAsync(id, dto);

        // Assert
        result.ShouldBeTrue();

        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
        existing.Title.ShouldBe(dto.Title);
        existing.Description.ShouldBe(dto.Description);
        _courseRepository.Received(1).Update(existing);
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_When_Not_Found_Should_Rollback_And_Return_False()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new UpdateCourseDto { Title = "Whatever", Description = null };
        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        // Act
        var result = await _sut.UpdateAsync(id, dto);

        // Assert
        result.ShouldBeFalse();
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
        _courseRepository.DidNotReceive().Update(Arg.Any<Course>());
    }

    [Fact]
    public async Task UpdateAsync_When_Exception_Thrown_Should_Rollback_And_ReThrow()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "X", Description = "Y" };
        var dto = new UpdateCourseDto { Title = "Z", Description = "W" };
        var ex = new Exception("unexpected");

        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));
        // Force SaveChangesAsync to throw
        _uow.When(u => u.SaveChangesAsync(Arg.Any<CancellationToken>())).Do(_ => throw ex);

        // Act & Assert
        var thrown = await Should.ThrowAsync<Exception>(() => _sut.UpdateAsync(id, dto));
        thrown.ShouldBeSameAs(ex);
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_When_Entity_Exists_Should_Remove_And_Return_True()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "ToDelete", Description = "Desc" };
        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.ShouldBeTrue();

        await _uow.Received(1).BeginTransactionAsync(Arg.Any<CancellationToken>());
        _courseRepository.Received(1).Remove(entity);
        await _uow.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_When_Not_Found_Should_Rollback_And_Return_False()
    {
        // Arrange
        var id = Guid.NewGuid();
        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(null));

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.ShouldBeFalse();
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
        _courseRepository.DidNotReceive().Remove(Arg.Any<Course>());
    }

    [Fact]
    public async Task DeleteAsync_When_Exception_Thrown_Should_Rollback_And_ReThrow()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Course { Id = id, Title = "X", Description = "Y" };
        var ex = new InvalidOperationException("boom");

        _courseRepository.GetByIdAsync(id).Returns(Task.FromResult<Course?>(entity));
        _uow.When(u => u.SaveChangesAsync(Arg.Any<CancellationToken>())).Do(_ => throw ex);

        // Act & Assert
        var thrown = await Should.ThrowAsync<InvalidOperationException>(() => _sut.DeleteAsync(id));
        thrown.ShouldBeSameAs(ex);
        await _uow.Received(1).RollbackAsync(Arg.Any<CancellationToken>());
    }

    #endregion
}
