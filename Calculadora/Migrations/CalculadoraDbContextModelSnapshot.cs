﻿// <auto-generated />
using System;
using Calculadora.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Calculadora.Migrations
{
    [DbContext(typeof(CalculadoraDbContext))]
    partial class CalculadoraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Calculadora.Models.Operaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExpresionMatematica")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Resultado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("fechaHora")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Operador", (string)null);
                });

            modelBuilder.Entity("Calculadora.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Calculadora.Models.Operaciones", b =>
                {
                    b.HasOne("Calculadora.Models.Usuario", "Usuario")
                        .WithMany("Operaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Calculadora.Models.Usuario", b =>
                {
                    b.Navigation("Operaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
