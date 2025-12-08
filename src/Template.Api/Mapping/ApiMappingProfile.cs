
using Mapster;
using Template.Api.Models.Course;
using Template.Api.Models.Enrollments;
using Template.Api.Models.Lesson;
using Template.Api.Models.User;
using Template.Application.Models.Courses;
using Template.Application.Models.Enrollments;
using Template.Application.Models.Lesson;
using Template.Application.Models.Users;
using Template.Domain.Entities;

namespace Template.Api.Mapping;

public class ApiMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // ============================
        // API → Application DTO
        // ============================
        config.NewConfig<CreateCourseRequest, CreateCourseDto>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<UpdateCourseRequest, UpdateCourseDto>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        // ============================
        // Application DTO → Domain Entity
        // ============================
        config.NewConfig<CreateCourseDto, Course>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<UpdateCourseDto, Course>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        // ============================
        // Domain Entity → Application DTO
        // ============================
        config.NewConfig<Course, CourseResultDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        // ============================
        // Application DTO → API Response
        // ============================
        config.NewConfig<CourseResultDto, CourseResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description);

        // ============================
        // Domain Entity → Extended CourseDetailsDto
        // ============================
        config.NewConfig<Course, CourseDetailsDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Lessons,
                 src => src.Lessons.Select(l => l.Title).ToList());

        // ===========================================================
        // USER MAPPINGS
        // ===========================================================

        // API → Application DTO
        config.NewConfig<CreateUserRequest, CreateUserDto>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.Password)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);

        config.NewConfig<UpdateUserRequest, UpdateUserDto>()
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash,
                 src => src.Password ?? null)
            .Map(dest => dest.FirstName, src => src.FirstName)
            .Map(dest => dest.LastName, src => src.LastName);

        // Application → Domain Entity
        config.NewConfig<CreateUserDto, User>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.PasswordHash)
            .Map(dest => dest.Profile,
                 src => new UserProfile
                 {
                     FirstName = src.FirstName,
                     LastName = src.LastName
                 });

        config.NewConfig<UpdateUserDto, User>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Profile)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.PasswordHash);

        // Domain Entity → Application DTO
        config.NewConfig<User, UserResultDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.FirstName, src => src.Profile.FirstName)
            .Map(dest => dest.LastName, src => src.Profile.LastName);

        // ===========================================================
        // LESSON MAPPINGS
        // ===========================================================

        // API → Application DTO
        config.NewConfig<CreateLessonRequest, CreateLessonDto>();
        config.NewConfig<UpdateLessonRequest, UpdateLessonDto>();

        // Application → Domain Entity
        config.NewConfig<CreateLessonDto, Lesson>()
            .Ignore(dest => dest.Id);
        config.NewConfig<UpdateLessonDto, Lesson>();

        // Domain → Application DTO
        config.NewConfig<Lesson, LessonResultDto>()
            .Map(dest => dest.CourseId, src => src.CourseId);

        // ===========================================================
        // ENROLLMENT MAPPINGS
        // ===========================================================

        // API → Application DTO
        config.NewConfig<EnrollUserRequest, EnrollUserDto>();

        // Domain → Application DTO
        config.NewConfig<Enrollment, EnrollmentResultDto>();
    }
}
