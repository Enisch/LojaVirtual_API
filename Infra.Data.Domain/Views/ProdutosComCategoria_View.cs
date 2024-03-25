using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Views
{
    /// <View>
    /// View será/Foi criada através do metodo migration.
    /// Pensei em utilizar DTO'S porém queria mostrar valores de duas clases e portanto seria dificil
    /// Usar VIEW's também estimula meu aprendizado pois nunca as usei com o EF Core.
    /// </Objetivo Criar views e reforçar criações através do Migration>
    public class ProdutosComCategoria_View
    {
        public int idProdutos {get; set;}
        public string? NomeProduto { get; set; }
        
        public double ValorProduto { get; set; }
        
        public int QtdProduto { get; set; }
       
        public string? NomeCategoria { get; set; }
        
       
    }
}
