namespace Template.Infrastructure.Persistence.Options;

public class PostgreSqlOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DbPassword { get; set; } = default!;
}
