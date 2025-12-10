using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="User"/> для Entity Framework Core.  
/// Определяет схему таблицы <c>users</c>, имена столбцов, ограничения,
/// индексы и комментарии, которые будут записаны в базу данных (если она их поддерживает).
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Конфигурирует модель <see cref="User"/> через <paramref name="builder"/>.
    /// Вызывается автоматически при построении модели EF Core (в <c>OnModelCreating</c>).
    /// </summary>
    /// <param name="builder">Построитель конфигурации для типа <see cref="User"/>.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", t => { t.HasComment("Пользователи платформы"); });

        builder.HasKey(u => u.Id)
            .HasName("pk_users_id");

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .HasComment("Идентификатор пользователя");

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("Email пользователя");

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .HasMaxLength(500)
            .IsRequired()
            .HasComment("Хэш пароля пользователя");

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_users_email")
            .HasFilter(null);

        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_user_profile_user_id");

        builder.Metadata
            .SetAnnotation("Description", "Основная таблица пользователей");
    }
}
