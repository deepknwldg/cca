namespace Template.Api.InternalClasses.Routing;

internal static class ApiRouting
{
    private const string BaseUrl = "template-service";

    public static class Courses
    {
        private const string Controller = $"{BaseUrl}/courses";

        public const string Create = $"{Controller}/create";
        public const string GetById = $"{Controller}/get-by-id/{{id:guid}}";
        public const string GetAll = $"{Controller}/get-all";
        public const string Update = $"{Controller}/update/{{id:guid}}";
        public const string Delete = $"{Controller}/delete/{{id:guid}}";
    }

    public static class Enrollments
    {
        private const string Controller = $"{BaseUrl}/enrollments";

        public const string Enroll = $"{Controller}/enroll";
        public const string Remove = $"{Controller}/remove/{{userId:guid}}/{{courseId:guid}}";
    }

    public static class Lessons
    {
        private const string Controller = $"{BaseUrl}/lessons";

        public const string Create = $"{Controller}/create";
        public const string GetById = $"{Controller}/get-by-id/{{id:guid}}";
        public const string GetByCourse = $"{Controller}/get-by-course/{{courseId:guid}}";
        public const string Update = $"{Controller}/update/{{id:guid}}";
        public const string Delete = $"{Controller}/delete/{{id:guid}}";
    }

    public static class Users
    {
        private const string Controller = $"{BaseUrl}/users";

        public const string Create = $"{Controller}/create";
        public const string GetById = $"{Controller}/get-by-id/{{id:guid}}";
        public const string GetAll = $"{Controller}/get-all";
        public const string Update = $"{Controller}/update/{{id:guid}}";
        public const string Delete = $"{Controller}/delete/{{id:guid}}";
    }
}
