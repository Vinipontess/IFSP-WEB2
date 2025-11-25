using Microsoft.EntityFrameworkCore;
using Models;


namespace WEB_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {   entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo);
                entity.Property(e => e.Concluida).IsRequired();
                entity.Property(e => e.DataCriacao).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

//Vinicius Pontes e Eduardo Barbosa
