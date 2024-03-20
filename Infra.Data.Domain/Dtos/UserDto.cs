using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Infra.Data.Domain.Dtos
{
    public class UserDto
    {
        public int idUsuario { get; set; }  
        
        [Required(ErrorMessage = "Digite seu nome."), StringLength(70, ErrorMessage = "O tamanho máximo do nome é 70 caracteres.")]
        public string? NomeUsuario { get; set; }
       
        [Required(ErrorMessage = "Digite um endereço de E-mail válido."),
            StringLength(90, ErrorMessage = "Digite um endereço de E-mail cujo seu tamanho seja menor que 80 caracters. ")]
        public string? EmailUsuario { get; set; }

        [StringLength(20,ErrorMessage ="A senha deve conter um máximo de 20 caracteres.")]
        [Required(ErrorMessage = "Digite uma senha."),NotNull]
        [NotMapped]
        public string? Password { get; set; }

    }
}
