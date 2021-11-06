using curso.api.Business.Entidades;
using curso.api.Business.Repositories;
using curso.api.Infraestrutura.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.api.Infraestruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
       private readonly CursosDbContext _context;

        public UsuarioRepository(CursosDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChanges();        
        }

        public Usuario ObterUsuario(string login)
        {
           return  _context.Usuario.FirstOrDefault(u => u.Login == login);
        }
    }
}
