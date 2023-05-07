using Calculadora.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Calculadora.Data
{
    public class CalculadoraDbContext:DbContext
    {
        public CalculadoraDbContext(DbContextOptions<CalculadoraDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Users { get; set; }
        public DbSet<Operaciones> Operador { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
