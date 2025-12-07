using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
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
