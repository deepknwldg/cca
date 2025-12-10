using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="UserProfile"/> для Entity Framework Core.  
/// Определяет схему таблицы <c>user_profiles</c>, имена столбцов, ограничения,
/// индексы и комментарии, которые будут записаны в базу данных (если она их поддерживает).
/// </summary>
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    /// <summary>
    /// Конфигурирует модель <see cref="UserProfile"/> через <paramref name="builder"/>.
    /// Вызывается автоматически при построении модели EF Core (в <c>OnModelCreating</c>).
    /// </summary>
    /// <param name="builder">Построитель конфигурации для типа <see cref="UserProfile"/>.</param>
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles", t => { t.HasComment("Расширенная информация о пользователе"); });

        builder.HasKey(p => p.Id)
            .HasName("pk_user_profiles_id");

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .HasComment("Идентификатор профиля");

        builder.Property(p => p.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("Имя пользователя");

        builder.Property(p => p.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("Фамилия пользователя");

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .HasComment("Идентификатор пользователя (FK)");

        builder.HasIndex(p => p.UserId)
            .IsUnique()
            .HasDatabaseName("ux_user_profiles_user_id");

        builder.Metadata.SetAnnotation("Description", "Профили пользователей");

    }
}
