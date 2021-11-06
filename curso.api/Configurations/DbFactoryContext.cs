using curso.api.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
    public class DbFactoryContext : IDesignTimeDbContextFactory<CursosDbContext>
    {
        public CursosDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<CursosDbContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BDCursoAPI");
            CursosDbContext context = new CursosDbContext(options.Options);
            return context;
        }
    }
}
