using curso.api.Business.Entidades;
using curso.api.Business.Repositories;
using curso.api.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace curso.api.Infraestruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursosDbContext _context;

        public CursoRepository(CursosDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Curso curso)
        {
            _context.Curso.Add(curso);        
        }

        public void Commit()
        {
            _context.SaveChanges();        
                }

        public IList<Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _context.Curso.Include(i=>i.Usuario).Where(c => c.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
