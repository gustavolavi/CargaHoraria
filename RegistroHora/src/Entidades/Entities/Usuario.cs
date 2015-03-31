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
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [Remote("LoginUnico", "Usuario", ErrorMessage = "Este Login já esta em uso!")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&*_]).{6,20})", ErrorMessage="A senha deve composta por simbolos,letras maiúsculas, minusculas é numeros")]
        public string Senha { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Confirmar Senha")]
        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [RegularExpression("^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&*_]).{6,20})", ErrorMessage = "A senha deve composta por simbolos,letras maiúsculas, minusculas é numeros")]
        public string ConfirmarSenha { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("EmailUnico", "Usuario", ErrorMessage = "Email já cadastrado! Entre em contato com Admin")]
        public string Email { get; set; }
    }
}
