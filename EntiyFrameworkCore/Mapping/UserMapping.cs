using EntiyFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntiyFrameworkCore.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnName("name")
                .HasColumnType("Varchar(50)").HasMaxLength(50).IsRequired();

            builder.Property(x => x.Email).HasColumnName("email")
                .HasColumnType("Varchar(100)").HasMaxLength(100).IsRequired();

            builder.Property(x => x.Password).HasColumnName("password")
                .HasColumnType("Varchar(50)").HasMaxLength(50).IsRequired();
        }
    }
}