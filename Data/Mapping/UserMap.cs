using CadastroApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroApi.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();
        }
    }
}
