using CadastroApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroApi.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Street)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.City)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Number)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(x => x.State)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.ZipCode)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.District)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);


            builder.HasOne(x => x.User)
                .WithMany(x => x.Addresses)
                .HasConstraintName("FK_User_Address");
        }
    }
}
