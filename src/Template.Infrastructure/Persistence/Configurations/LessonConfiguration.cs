using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
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
