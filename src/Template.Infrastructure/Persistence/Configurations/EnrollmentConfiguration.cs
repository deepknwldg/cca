using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("enrollments", t => { t.HasComment("Связь пользователей с курсами (многие ко многим)"); });

        builder.HasKey(e => new { e.UserId, e.CourseId })
            .HasName("pk_enrollments");

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasComment("Идентификатор пользователя (FK)");

        builder.Property(e => e.CourseId)
            .HasColumnName("course_id")
            .HasComment("Идентификатор курса (FK)");

        builder.Property(e => e.EnrolledAt)
            .HasColumnName("enrolled_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasComment("Дата подписки пользователя на курс");

        builder.HasIndex(e => e.CourseId)
            .HasDatabaseName("ix_enrollments_course_id");

        builder.HasIndex(e => e.EnrolledAt)
            .HasDatabaseName("ix_enrollments_enrolled_at");

        builder.HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("fk_enrollments_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .HasConstraintName("fk_enrollments_course_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.SetAnnotation("Description", "M:N связь пользователей и курсов");
    }
}
