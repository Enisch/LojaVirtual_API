using Context.ContextForDb;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        //Get PRoducts methods
        public async Task<List<Produtos>> GetProdutos()
        {
            return await cc.Produtos.OrderByDescending(x=> x.Name).ToListAsync();
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

        

        public async Task<List<Produtos>> GetProdutosByCategoria(Categoria_produtos categoria)//I 'll pass it to the controller later;
        {
            var ProductByCategoria = await cc.Produtos.Where(x => x.CategoriaDoProduto == categoria.IdCategoria).ToListAsync();
            return ProductByCategoria;
        }
    }
}
