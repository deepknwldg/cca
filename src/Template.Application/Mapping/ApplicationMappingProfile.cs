using AutoMapper;
using Template.Application.Models.Common;
using Template.Application.Models.Courses;
using Template.Application.Models.Enrollments;
using Template.Application.Models.Lessons;
using Template.Application.Models.Users;
using Template.Domain.Entities;

namespace Template.Application.Mapping;

/// <summary>
/// Профиль AutoMapper, который описывает преобразования между
/// DTO‑слоем (Application) и доменными сущностями (Domain) и наоборот.
/// </summary>
public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        // ============================
        // Application DTO → Domain Entity
        // ============================
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => new UserProfile
            {
                FirstName = src.FirstName,
                LastName = src.LastName
            }));

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Profile, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<CreateCourseDto, Course>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<UpdateCourseDto, Course>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<CreateLessonDto, Lesson>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<UpdateLessonDto, Lesson>();

        // ============================
        // Domain Entity → Application DTO
        // ============================
        CreateMap<User, UserResultDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Profile.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Profile.LastName));

        CreateMap<Course, CourseResultDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Course, CourseDetailsDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons.Select(l => l.Title).ToList()));

        CreateMap<Lesson, LessonResultDto>()
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId));

        CreateMap<Enrollment, EnrollmentResultDto>();

        // ============================
        // Generic PagedResult
        // ============================
        CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
    }
}
