using Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        Usuario Login(string logim, string senha);
        Collection<string> ListaDeLogins();
        Collection<string> ListaDeEmails();
    }
}
