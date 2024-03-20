using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    [Table("ContaUsuario")]
    public class ContaUsuario
    {
        [Key, Column("idConta")]
        public int idConta { get; set; }
        [Column("Saldo"),AllowNull]//Permite que o usuario transfirá dinheiro e o deixe armazenado aqui. 
        public double Saldo { get; set; }
        [ForeignKey("Fk_UserID"), Column("IdUsuario"), Required]
        public int IDUsuario { get; set; }

        [Column("QtdDeProdutosComprados"), AllowNull]
        public int QtdDeProdutosComprados { get; set; }

        [Column("DescontosParaUsuario"), AllowNull]
        public double Descontos { get; set; }

        [Column("RankUsuario"), Required]
        public string? RankUser { get; set; }

    }
}
