using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
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
