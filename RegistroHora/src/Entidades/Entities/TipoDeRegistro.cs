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
        [Required]
        public string Tipo { get; set; }

        public virtual ICollection<CargaHoraria> CargaHorarias { get; set; }
    }
}
