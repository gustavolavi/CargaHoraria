using System;
using System.Linq;
using System.Text;
using Entidades.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entidades.Interfaces.Repositorio;
namespace Infra.Repositorio
{
    public class CargaHorariaRepositorio : RepositorioBase<CargaHoraria>, ICargaHorariaRepositorio
    {
        public List<CargaHoraria> GetAllByUser(int UsuarioId)
        {
            return db.CargaHoraria.Where(x=>x.UsuarioId == UsuarioId).OrderByDescending(x => x.DataHora).ToList();
        }

        public override void Add(CargaHoraria obj)
        {
            db.CargaHoraria.Add(obj);
            db.SaveChanges();
        }

        public CargaHoraria GetLastTipoId(int idDoUsuario)
        {
            CargaHoraria retorno = new CargaHoraria();
            var query = (from CargaHoraria c in db.CargaHoraria where c.UsuarioId == idDoUsuario orderby c.DataHora select c).ToList();
            if (query.Count > 0)
                retorno = query.Last();
            return retorno;
        }
    }
}
