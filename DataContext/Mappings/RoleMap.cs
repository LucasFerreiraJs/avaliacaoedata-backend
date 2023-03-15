using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace api.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            // tabela
            builder.ToTable("Role");
    
            // chave
            builder.HasKey(x => x.RoleID);

            //// Identity
            //builder.Property(x => x.RoleID)
            //    .ValueGeneratedOnAdd()
            //    .UseIdentityColumn();


            // Nome
            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50);

            // Índices
            builder
                .HasIndex(x => x.Name, "IX_Role_Name")
                .IsUnique();

        }
    }
}
