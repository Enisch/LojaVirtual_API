using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IProdutos
    {
        public Task<List<Produtos>> GetProdutos();
        public Task<List<Produtos>> GetProdutos(string nome);
        public Task<Produtos> GetProdutos(int id);
        public Task<List<Produtos>> GetProdutosByCategoria(Categoria_produtos categoria);
        public Task<bool> BuyProdutos(int id,int IdUsuario);
        public Task InstanciarControleDeVendas(int idusuario,int IdProduto);

    }
}
