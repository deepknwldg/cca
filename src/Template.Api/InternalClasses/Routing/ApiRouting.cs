namespace Template.Api.InternalClasses.Routing;

internal static class ApiRouting
{
    private const string BaseUrl = "template-service";

    public static class Courses
    {
        public const string Create = $"{BaseUrl}/courses/create";
        public const string GetById = $"{BaseUrl}/courses/get-by-id";
        public const string GetAll = $"{BaseUrl}/courses/get-all";
        public const string Update = $"{BaseUrl}/courses/update";
        public const string Delete = $"{BaseUrl}/courses/delete";
    }

    public static class Enrollments
    {
        public const string Enroll = $"{BaseUrl}/enrollments/enroll";
        public const string Remove = $"{BaseUrl}/enrollments/remove";
    }

    public static class Lessons
    {
        public const string Create = $"{BaseUrl}/lessons/create";
        public const string GetById = $"{BaseUrl}/lessons/get-by-id";
        public const string GetByCourse = $"{BaseUrl}/lessons/get-by-course";
        public const string Update = $"{BaseUrl}/lessons/update";
        public const string Delete = $"{BaseUrl}/lessons/delete";
    }

    public static class Users
    {
        public const string Create = $"{BaseUrl}/users/create";
        public const string GetById = $"{BaseUrl}/users/get-by-id";
        public const string GetAll = $"{BaseUrl}/users/get-all";
        public const string Update = $"{BaseUrl}/users/update";
        public const string Delete = $"{BaseUrl}/users/delete";
    }
}
