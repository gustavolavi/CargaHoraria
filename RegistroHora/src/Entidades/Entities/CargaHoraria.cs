using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entidades.Entities
{
    public class CargaHoraria
    {

        [Key]
        public int CargaHorariaId { get; set; }
        
        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy . HH:mm}")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio para calculo de horas")]
        public int TipoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual TipoDeRegistro TipoDeRegisto { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Data é um campo obrigratorio")]
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public virtual DateTime Data { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Hora é um campo obrigratorio")]
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        public virtual DateTime Hora { get; set; }

        public void tranformarDataHora()
        {
            Data = DataHora;
            Hora = DataHora;
        }

        public void DaraComHora()
        {
            DateTime date = Convert.ToDateTime(Data);
            DateTime time = Convert.ToDateTime(Hora);
            DataHora = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }
    }
}
