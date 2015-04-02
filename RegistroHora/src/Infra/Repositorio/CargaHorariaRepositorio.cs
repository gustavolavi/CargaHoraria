using Entidades.Entities;
using Entidades.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int GetLastTipoId()
        {
            var all = GetAll();
            CargaHoraria ch = null;
            foreach (var item in all)
            {
                if (ch != null)
                {
                    if (ch.DataHora < item.DataHora)
                    {
                        ch = item;
                    }
                }
                else
                {
                    ch = item;
                }
            }
            return ch.TipoId;
        }
    }
}
