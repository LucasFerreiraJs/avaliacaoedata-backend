using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Mappings
{
    public class RegionMap: IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder) {

            builder.ToTable("Region");

            builder
                .HasKey(x => x.RegionID);

            builder.Property(x => x.RegionID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);


            // Índices
            builder
                .HasIndex(x => x.Name, "IX_Regional_Name")
                .IsUnique();
        }
    }
}
