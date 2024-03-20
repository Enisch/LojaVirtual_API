using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    [Table("Produtos")]
    public class Produtos
    {
        [Key,Column("idProdutos"),Required]
        public int Id { get; set; }
        [Column("NomeProduto"),Required]
        public string? Name { get; set; }
        [Column("ValorProduto"),Required]
        public double ValorDoProduto { get; set; }
        [Column("QtdProduto"),Required]
        public int QtdProduto { get; set; }
        [ForeignKey("Fk_Categoria"),Column("IdCategoria")]
        public int CategoriaDoProduto { get; set;}
    }
}
