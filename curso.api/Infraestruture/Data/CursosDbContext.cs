using curso.api.Business.Entidades;
using curso.api.Infraestrutura.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace curso.api.Infraestrutura.Data
{
    public class CursosDbContext:DbContext
    {
        public CursosDbContext(DbContextOptions<CursosDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMappings());
            modelBuilder.ApplyConfiguration(new UsuarioMappings());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Curso> Curso { get; set; }
    }
}
