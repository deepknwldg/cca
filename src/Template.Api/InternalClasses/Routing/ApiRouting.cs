namespace Template.Api.InternalClasses.Routing;

/// <summary>
/// Содержит константы URL‑маршрутов, используемых в API проекта.
/// </summary>
internal static class ApiRouting
{
    private const string BaseUrl = "template-service";

    /// <summary>
    /// Маршруты, относящиеся к курсам.
    /// </summary>
    public static class Courses
    {
        private const string Controller = $"{BaseUrl}/courses";

        public const string Create = $"{Controller}/create";
        public const string GetById = $"{Controller}/get-by-id/{{id:guid}}";
        public const string GetAll = $"{Controller}/get-all";
        public const string Update = $"{Controller}/update/{{id:guid}}";
        public const string Delete = $"{Controller}/delete/{{id:guid}}";
    }

    /// <summary>
    /// Маршруты, относящиеся к записям пользователей на курсы (enrollments).
    /// </summary>
    public static class Enrollments
    {
        private const string Controller = $"{BaseUrl}/enrollments";

        public const string Enroll = $"{Controller}/enroll";
        public const string Remove = $"{Controller}/remove/{{userId:guid}}/{{courseId:guid}}";
    }

    /// <summary>
    /// Маршруты, относящиеся к урокам.
    /// </summary>
    public static class Lessons
    {
        private const string Controller = $"{BaseUrl}/lessons";

        public const string Create = $"{Controller}/create";
        public const string GetById = $"{Controller}/get-by-id/{{id:guid}}";
        public const string GetByCourse = $"{Controller}/get-by-course/{{courseId:guid}}";
        public const string Update = $"{Controller}/update/{{id:guid}}";
        public const string Delete = $"{Controller}/delete/{{id:guid}}";
    }

    /// <summary>
    /// Маршруты, относящиеся к пользователям.
    /// </summary>
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
