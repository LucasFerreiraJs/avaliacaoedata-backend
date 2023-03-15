using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.ID);


            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(50)
                .HasColumnType("nvarchar");


            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(50)
                .HasColumnType("nvarchar");


            builder.Property(x => x.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .HasColumnType("nvarchar");


            builder.Property(x => x.UserLogin)
                .IsRequired()
                .HasColumnName("UserLogin")
                .HasMaxLength(50)
                .HasColumnType("nvarchar");


            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasColumnType("nvarchar(max)");

            builder
                .HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(x => x.RoleID)
                .HasConstraintName("FK_Role_User");

            builder.Property(x => x.RhCode)
                 .HasColumnName("RhCode")
                 .HasColumnType("int");

            builder
                 .HasOne(x => x.Region)
                 .WithMany(r => r.Users)
                 .HasForeignKey(x => x.RegionID)
                 .HasConstraintName("FK_Region_User");

            builder.Property(x => x.DateAction)
                .IsRequired()
                .HasColumnName("DateAction")
                .HasColumnType("Datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UserAction)
                .IsRequired()
                .HasColumnName("UserAction")
                .ValueGeneratedOnUpdate();


            builder.Property(x => x.LastAccess)
                .HasColumnName("LastAccess")
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();
        }   
    }
}
