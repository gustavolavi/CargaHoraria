using System;
using System.Linq;
using System.Text;
using Entidades.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entidades.Interfaces.Repositorio;

namespace Infra.Repositorio
{
    public class TipoDeRegistroRepositorio : RepositorioBase<TipoDeRegistro>, ITipoDeRegistroRepositorio
    {
    }
}
