using GestionTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Context
{
    public class GestorTareasContext:DbContext
    {
        public GestorTareasContext(DbContextOptions<GestorTareasContext> options) : base(options) { }

        public DbSet<Tarea> Tareas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tarea>().ToTable("Tarea");
            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.Property(b => b.Description).HasMaxLength(255).IsRequired();
                tarea.Property(b => b.Status).IsRequired();
            });
        }
    }
}
