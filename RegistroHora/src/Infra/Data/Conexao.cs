using System;
using System.Linq;
using System.Text;
using Entidades.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infra.Data
{
    public class Conexao : DbContext
    {
        public Conexao()
            : base("teste") 
        {
        }
        public DbSet<CargaHoraria> CargaHoraria { get; set; }
        public DbSet<TipoDeRegistro> TipoDeRegistro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
