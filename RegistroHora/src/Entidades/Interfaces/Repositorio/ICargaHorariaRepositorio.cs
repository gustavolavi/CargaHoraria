using System;
using System.Linq;
using System.Text;
using Entidades.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Entidades.Interfaces.Repositorio
{
    public interface ICargaHorariaRepositorio : IRepositorioBase<CargaHoraria>
    {
        CargaHoraria GetLastTipoId(int idDoUsuario);
        List<CargaHoraria> GetAllByUser(int UsuarioId);
    }
}
