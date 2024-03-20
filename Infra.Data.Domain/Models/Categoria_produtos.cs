using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    [Table("Categoria_Produtos")]
    public  class Categoria_produtos
    {

        [Key,Column("IdCategoria")]
        public int IdCategoria {  get; set; }

        [Column("NomeCategoria"),Required]
        public string? Categoria { get; set;}
        [Column("DescriçãoCategoria")]
        public string? DescriçãoCategoria { get; set;}
        
    }
}
