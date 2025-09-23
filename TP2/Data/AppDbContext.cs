using CBTSWE2_TP02.Models;
using Microsoft.EntityFrameworkCore;

namespace CBTSWE2_TP02.Data
{
    //Desenvolvido por Laysa Bernardes e Lucas Lopes
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BL> BLs { get; set; }
        public DbSet<Container> Containers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BL>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Numero).IsRequired();
                entity.Property(b => b.Consignee).IsRequired();
                entity.Property(b => b.Navio).IsRequired();
            });

            modelBuilder.Entity<Container>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Numero)
                      .HasMaxLength(11)
                      .IsRequired();

                entity.HasIndex(c => c.Numero).IsUnique();

                entity.Property(c => c.Tipo)
                      .HasMaxLength(10)
                      .IsRequired()
                      .HasAnnotation("Relational:CheckConstraint", "[Tipo] IN ('Dry', 'Reefer')");

                entity.Property(c => c.Tamanho)
                      .IsRequired()
                      .HasAnnotation("Relational:CheckConstraint", "[Tamanho] IN (20, 40)");
            });
        }
    }
}
