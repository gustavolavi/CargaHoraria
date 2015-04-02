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



        public bool ValidarLogin(string login)
        {
            var query = (from c in db.Usuario where c.Login == login select c.Login).FirstOrDefault();
            if (query != null)
                return false;

            return true;

        }

        public bool ValidarEmail(string Email)
        {
            var query = (from c in db.Usuario where c.Email == Email select c.Email).FirstOrDefault();
            if (query != null)
                return false;
            return true;
        }
    }
}
