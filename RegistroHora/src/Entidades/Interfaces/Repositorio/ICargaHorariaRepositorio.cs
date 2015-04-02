using Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces.Repositorio
{
    public interface ICargaHorariaRepositorio : IRepositorioBase<CargaHoraria>
    {
        int GetLastTipoId();
        List<CargaHoraria> GetAllByUser(int UsuarioId);
    }
}
