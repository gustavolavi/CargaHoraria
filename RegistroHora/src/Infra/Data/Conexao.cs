using Entidades.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class Conexao : DbContext
    {
        public Conexao()
            : base("local") 
        {
        }

        public DbSet<CargaHoraria> CargaHoraria { get; set; }
        public DbSet<TipoDeRegistro> TipoDeRegistro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}
