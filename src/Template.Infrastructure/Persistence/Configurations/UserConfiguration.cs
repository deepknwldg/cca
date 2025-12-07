using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
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

        builder.Property(u => u.Email)
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
