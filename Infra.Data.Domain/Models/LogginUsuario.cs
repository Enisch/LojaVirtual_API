using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    public class LogginUsuario
    {
        [Required(ErrorMessage ="Digite um Email válido."),StringLength(80,ErrorMessage ="O E-mail deve conter no máximo 80 caracteres.")]
        public string? EmailUser { get; set; }

        [Required(ErrorMessage ="Digite uma senha válida."),StringLength(20,ErrorMessage ="A senha deve conter no máximo 20 caracteres.")]
        public string? PAsswordUser { get; set; }

    }
}
