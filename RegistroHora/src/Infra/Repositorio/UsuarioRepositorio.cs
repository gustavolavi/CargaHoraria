using Entidades.Entities;
using Entidades.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public Usuario Login(string login, string senha)
        {
            Usuario usuario = new Usuario();
            usuario = db.Usuario.Where(x => x.Login == login).SingleOrDefault();
            if (usuario == null)
            {
                usuario = db.Usuario.Where(x => x.Email == login).SingleOrDefault();
            }
            if (usuario != null)
            {
                if (usuario.Senha == senha)
                {
                    return usuario;
                }
            }
            return null;
        }



        public Collection<string> ListaDeLogins()
        {
            Collection<string> retorno = new Collection<string>();
            var query = (from u in db.Usuario select u.Login).ToList();
            foreach (var i in query) {
                retorno.Add(i);
            }
            return retorno;
        }

        public Collection<string> ListaDeEmails()
        {
            Collection<string> retorno = new Collection<string>();
            var query = (from u in db.Usuario select u.Email).ToList();
            foreach (var i in query)
            {
                retorno.Add(i);
            }
            return retorno;
        }

    }
}
