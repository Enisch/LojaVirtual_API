using Context.ContextForDb;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Infra.Data.Domain.Pagination;
using Infra.Data.Domain.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioApplication.Helper;

namespace UsuarioApplication.Repositories
{
    public class ProdutosRepository : IProdutos
    {
        private readonly ContextClass cc;

        public ProdutosRepository(ContextClass cc)
        {
            this.cc = cc;
        }
        public async Task<bool> BuyProdutos(int idProduto,int idUSuario)
        {
            var user = await cc.contaUsuarios.Where(x=> x.IDUsuario == idUSuario).FirstOrDefaultAsync();
            var Produto = await cc.Produtos.Where(x=> x.Id== idProduto).FirstOrDefaultAsync();

            if (Produto!.QtdProduto == 0)
                return Task.CompletedTask.IsCanceled;

            if (Produto.ValorDoProduto > user!.Saldo)
                return false;

            await InstanciarControleDeVendas(idUSuario, idProduto);

            Produto.QtdProduto -= 1;
            cc.Entry(Produto).State = EntityState.Modified;
            await cc.SaveChangesAsync();
            user.Saldo-= Produto.ValorDoProduto;
            user.QtdDeProdutosComprados += 1;
            cc.Entry(user).State = EntityState.Modified;
            await cc.SaveChangesAsync();

            return true;
        }

        public async Task InstanciarControleDeVendas(int idusuario, int IdProduto)
        {
           /* var TempoString = DateTime.Now.ToString("dd/M/yyyy");
            var tempo = DateTime.Parse(TempoString);
            var T = DateOnly.FromDateTime(tempo);*/

            ControleDeVendas Cv = new ControleDeVendas
            {
                FkIdProdutos = IdProduto,
                FKidUsuario = idusuario,
                VendaData = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            await cc.AddAsync(Cv);
            await cc.SaveChangesAsync();
        }



        //Get Products methods

        public async Task<List<Produtos>> GetProdutosSemPagination()
        {
            var Produtos = await cc.Produtos.OrderBy(x => x.Name).ToListAsync();
            return Produtos;
        }

        public async Task<PagedList<Produtos>> GetProdutos(int pageSize,int pageNumber)//Com paginação(Teste) para obter melhor performance
        {
            var query = cc.Produtos.OrderBy(x => x.Name).AsQueryable();
            return await PaginationHelper.CreateList<Produtos>(query, pageSize, pageNumber);
        }

        public async Task<List<Produtos>> GetProdutos(string nome)
        {
            var ProductByName = await cc.Produtos.Where(x => x.Name!.ToLower().Contains(nome.ToLower())).ToListAsync();

            return ProductByName!;
        }

        public async Task<Produtos> GetProdutos(int id)
        {
            var ProductByID = await cc.Produtos.Where(x=> x.Id ==id).FirstOrDefaultAsync();
            return ProductByID!;
        }

        

        public async Task<List<Produtos>> GetProdutosByCategoria()
        {// Retorna os produtos pela categoria e ordem alfabetica
            var ProductByCategoria = await cc.Produtos.OrderBy(x => x.CategoriaDoProduto).ThenBy(x=> x.Name).ToListAsync();
            return ProductByCategoria!;
        }

        public async Task<bool> CadastrarProdutos(Produtos produtos)
        {
            if(produtos != null)
            {
                await cc.AddAsync(produtos);
                await cc.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProdutosComCategoria_View>> GetByCategoriesUSing_VIEW()
        {
            var Products = await cc.ProdutosCategoria_View.OrderBy(x=> x.NomeCategoria).ThenBy(x=> x.NomeProduto).ToListAsync();

            return Products;
        }

    }
}
