﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApiDataContext))]
    [Migration("20230311162346_ajustePasswordtabelaUser")]
    partial class ajustePasswordtabelaUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("api.Models.Region", b =>
                {
                    b.Property<int>("RegionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegionID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Name");

                    b.HasKey("RegionID");

                    b.HasIndex(new[] { "Name" }, "IX_Regional_Name")
                        .IsUnique();

                    b.ToTable("Region", (string)null);
                });

            modelBuilder.Entity("api.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Name");

                    b.HasKey("RoleID");

                    b.HasIndex(new[] { "Name" }, "IX_Role_Name")
                        .IsUnique();

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar")
                        .HasColumnName("CPF");

                    b.Property<DateTime>("DateAction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Datetime")
                        .HasColumnName("DateAction")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Email");

                    b.Property<DateTime>("LastAccess")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("DateTime")
                        .HasColumnName("LastAccess")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.Property<int>("RhCode")
                        .HasColumnType("int")
                        .HasColumnName("RhCode");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserAction")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserAction");

                    b.Property<string>("UserLogin")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar")
                        .HasColumnName("UserLogin");

                    b.HasKey("ID");

                    b.HasIndex("RegionID");

                    b.HasIndex("RoleID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.HasOne("api.Models.Region", "Region")
                        .WithMany("Users")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Region_User");

                    b.HasOne("api.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Role_User");

                    b.Navigation("Region");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("api.Models.Region", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("api.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
