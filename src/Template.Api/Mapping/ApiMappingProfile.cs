
using AutoMapper;
using Template.Api.Models.Common;
using Template.Api.Models.Course;
using Template.Api.Models.Enrollments;
using Template.Api.Models.Lesson;
using Template.Api.Models.User;
using Template.Application.Models.Courses;
using Template.Application.Models.Enrollments;
using Template.Application.Models.Lessons;
using Template.Application.Models.Users;
using Template.Domain.ValueObjects;

namespace Template.Api.Mapping;

/// <summary>
/// Профиль AutoMapper, описывающий все преобразования между
/// объектами API‑слоя (запросы/ответы) и DTO‑слоя
/// бизнес‑логики (Application).
/// </summary>
public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // ============================
        // API Request → Application DTO
        // ============================
        CreateMap<CreateUserRequest, CreateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.GetHashCode()));

        CreateMap<UpdateUserRequest, UpdateUserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

        CreateMap<CreateCourseRequest, CreateCourseDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<UpdateCourseRequest, UpdateCourseDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<CreateLessonRequest, CreateLessonDto>();
        CreateMap<UpdateLessonRequest, UpdateLessonDto>();
        CreateMap<EnrollUserRequest, EnrollUserDto>();

        // ============================
        // Application DTO → API Response
        // ============================
        CreateMap<CourseResultDto, CourseResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        // ============================
        // Pagination
        // ============================
        CreateMap<PagedRequest, PagingParams>();
    }
}
