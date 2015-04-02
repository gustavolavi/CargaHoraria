using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entities
{
    public class TipoDeRegistro
    {
        [Key]
        public int TipoId { get; set; }
         //entrada e saida
        [Required]
        public string Tipo { get; set; }
        //empresa ou almoço
        //[Required]
        //public string Modo { get; set; }

        public virtual ICollection<CargaHoraria> CargaHorarias { get; set; }
    }
}
