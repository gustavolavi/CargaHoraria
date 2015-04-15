using System;
using System.Linq;
using System.Text;
using Entidades.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Entidades.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Usuario Login(string logim, string senha);
       bool ValidarLogin(string login);
       bool ValidarEmail(string Email);
    }
}
