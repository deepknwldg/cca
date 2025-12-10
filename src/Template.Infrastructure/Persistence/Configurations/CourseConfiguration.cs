using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Course"/> для Entity Framework Core.  
/// Определяет схему таблицы <c>courses</c>, имена столбцов, ограничения,
/// индексы и комментарии, которые будут записаны в базу данных (если она их поддерживает).
/// </summary>
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    /// <summary>
    /// Конфигурирует модель <see cref="Course"/> через <paramref name="builder"/>.
    /// Вызывается автоматически при построении модели EF Core (в <c>OnModelCreating</c>).
    /// </summary>
    /// <param name="builder">Построитель конфигурации для типа <see cref="Course"/>.</param>
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses", t => { t.HasComment("Учебные курсы"); });

        builder.HasKey(c => c.Id)
            .HasName("pk_courses_id");

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .HasComment("Идентификатор курса");

        builder.Property(c => c.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("Название курса");

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(2000)
            .HasComment("Описание курса");

        builder.HasIndex(c => c.Title)
            .HasDatabaseName("ix_courses_title");

        builder.Metadata.SetAnnotation("Description", "Таблица курсов");
    }
}
