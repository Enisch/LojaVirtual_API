using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    [Table("ControleVendas")]
    public class ControleDeVendas
    {
        [Key,Column("IdControle")]
        public int IdControle { get; set; }
        [Column("DataVenda")]
        public DateOnly VendaData {  get; set; }
        [ForeignKey("Fk_IdProdutos"),Column("idProdutos")]
        public int FkIdProdutos { get; set; }
        [ForeignKey("Fk_usuario"),Column("FKidUsuario")]
        public int FKidUsuario {  get; set; }

    }
}
