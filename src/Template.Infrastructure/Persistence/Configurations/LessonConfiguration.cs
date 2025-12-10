using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Lesson"/> для Entity Framework Core.  
/// Определяет схему таблицы <c>lessons</c>, имена столбцов, ограничения,
/// индексы и комментарии, которые будут записаны в базу данных (если она их поддерживает).
/// </summary>
public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    /// <summary>
    /// Конфигурирует модель <see cref="Lesson"/> через <paramref name="builder"/>.
    /// Вызывается автоматически при построении модели EF Core (в <c>OnModelCreating</c>).
    /// </summary>
    /// <param name="builder">Построитель конфигурации для типа <see cref="Lesson"/>.</param>
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("lessons", t =>
        {
            t.HasComment("Уроки, относящиеся к курсу");
        });

        builder.HasKey(l => l.Id)
            .HasName("pk_lessons_id");

        builder.Property(l => l.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .HasComment("Идентификатор урока");

        builder.Property(l => l.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired()
            .HasComment("Название урока");

        builder.Property(l => l.Content)
            .HasColumnName("content")
            .HasColumnType("text")
            .HasComment("Содержимое урока");

        builder.Property(l => l.CourseId)
            .HasColumnName("course_id")
            .IsRequired()
            .HasComment("Идентификатор курса (FK)");

        builder.HasIndex(l => l.CourseId)
            .HasDatabaseName("ix_lessons_course_id");

        builder.HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_lessons_course_id");

        builder.Metadata.SetAnnotation("Description", "Таблица уроков");
    }
}
