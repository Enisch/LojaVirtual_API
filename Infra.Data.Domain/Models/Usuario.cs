using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int idUsuario { get; set; }

        [Column("nomeUsuario")]
        [Required(ErrorMessage = "Digite seu nome."),StringLength(60,ErrorMessage ="O tamanho máximo do nome é 60 caracteres.")]
        public  string? NomeUsuario { get; set; }
        [Column("EmailUsuario")]
        [Required(ErrorMessage ="Digite um endereço de E-mail válido."),
            StringLength(80,ErrorMessage ="Digite um endereço de E-mail cujo seu tamanho seja menor que 80 caracters. ")]
        public string? EmailUsuario { get;set; }

        [Column("SenhaUsuario")]
        [Required(ErrorMessage ="Digite uma senha.")]
        public byte[]? Senha { get; set; }

        [Column("IsAdmin")]
        public bool IsAdmin { get; set; }   

    }
}
