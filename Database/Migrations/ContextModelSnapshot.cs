﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Database.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Database.Models.Productora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Productoras");
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ProductoraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoraId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("Database.Models.SeriesGeneros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("SerieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.HasIndex("SerieId");

                    b.ToTable("SeriesGeneros");
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.HasOne("Database.Models.Productora", "Productora")
                        .WithMany("Series")
                        .HasForeignKey("ProductoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Productora");
                });

            modelBuilder.Entity("Database.Models.SeriesGeneros", b =>
                {
                    b.HasOne("Database.Models.Genero", "genero")
                        .WithMany("Serie")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Serie", "serie")
                        .WithMany("Genero")
                        .HasForeignKey("SerieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("genero");

                    b.Navigation("serie");
                });

            modelBuilder.Entity("Database.Models.Genero", b =>
                {
                    b.Navigation("Serie");
                });

            modelBuilder.Entity("Database.Models.Productora", b =>
                {
                    b.Navigation("Series");
                });

            modelBuilder.Entity("Database.Models.Serie", b =>
                {
                    b.Navigation("Genero");
                });
#pragma warning restore 612, 618
        }
    }
}
