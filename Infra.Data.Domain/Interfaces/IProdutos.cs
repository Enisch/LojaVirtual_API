using Infra.Data.Domain.Models;
using Infra.Data.Domain.Pagination;
using Infra.Data.Domain.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IProdutos
    {
        public Task<List<Produtos>> GetProdutosSemPagination();
        public Task<PagedList<Produtos>> GetProdutos(int pageSize,int pageNumber);
        public Task<List<Produtos>> GetProdutos(string nome);
        public Task<Produtos> GetProdutos(int id);
        public Task<List<ProdutosComCategoria_View>> GetByCategoriesUSing_VIEW();//Teste
        public Task<List<Produtos>> GetProdutosByCategoria();//Não implementado ainda
        public Task<bool> BuyProdutos(int id,int IdUsuario);
        public Task InstanciarControleDeVendas(int idusuario,int IdProduto);
        public Task<bool> CadastrarProdutos(Produtos produtos);

    }
}
