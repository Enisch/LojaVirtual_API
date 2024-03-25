using Context.ContextForDb;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Infra.Data.Domain.Pagination;
using LojaVirtual_Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LojaVirtual_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/Conta/Usuario")]
    public class ComprasController : Controller
    {
        private readonly IProdutos produtosMethods;
        private readonly ContextClass cc;
        public ComprasController(IProdutos produtosMethods, ContextClass cc)
        {
            this.produtosMethods = produtosMethods;
            this.cc = cc;
        }

        //Metodo do controller que realiza a compra do 'produto', através de injeção de independência
        [HttpPatch("Comprar Produto:")]
        public async Task<ActionResult> BuyProduct(int idDoPRoduto) 
        {
            var WasPurchasedOrNot = await produtosMethods.BuyProdutos(idDoPRoduto, TakeUserByID());
            if (!WasPurchasedOrNot)
                return BadRequest("A compra não pode ser efetuada.\nSaldo ou quantidade de produtos insuficientes.");

            var UserAccount = await cc.contaUsuarios.Where(x => x.IDUsuario == TakeUserByID()).FirstOrDefaultAsync();
            var nome = Convert.ToString(User.FindFirst("nomeUsuario")!.Value);
            return Ok($"Compra realizada.\nSr(a){nome}, seu Novo saldo é de: R$"+ UserAccount!.Saldo +"\nAté a próxima compra.");
        }

        [HttpGet("Buscar Produto:")]//Teste com pagination
        public async Task<ActionResult> GetProducts
            (int id,string? nome,string? BuscarPorCategoria,string? BuscaPorCategoriaUsandoView) 
        {
            if(id >0)//Busca do produto pelo Id
            {
                var ProductsByID = await produtosMethods.GetProdutos(id);
                if(ProductsByID == null)
                    return NotFound("Nenhum item com esse Id.");
                return Ok(ProductsByID);      
            }
            if(nome != null)//Busca do produto pelo nome.
            {
                var productsByName = await produtosMethods.GetProdutos(nome);
                if(productsByName == null)
                    return NotFound("Nenhum item com essa nomenclatura.");
                return Ok(productsByName);
            }
            if(BuscarPorCategoria != null)//Desde de que, a string não seja null, a busca será feita em ordem ASC;
            {
                var ProductByCategories = await produtosMethods.GetProdutosByCategoria();
                return Ok(ProductByCategories);
            }

            if(BuscaPorCategoriaUsandoView != null)
            {
                var productByCategories = await produtosMethods.GetByCategoriesUSing_VIEW();
                return Ok(productByCategories);
            }
            //Busca padrão order by Ascending.
            var produtos = await produtosMethods.GetProdutosSemPagination();
            return Ok(produtos);
        }

        [HttpGet("Busca com paginação")]
        public async Task<ActionResult> GetProdutos([FromQuery]PaginationParameters paginationParameters)
        {
            var List = await produtosMethods.GetProdutos(paginationParameters.PageSize, paginationParameters.PageNumber);

            Response.AddPaginationHeader(new PaginationHeader(List.TotalCount, List.PageSize, List.CurrentPage, List.TotalPage));

            return Ok(List);
        }

        [HttpPost("Cadastrar Produtos")]//Só Admin
        public async Task<ActionResult<string>> RegistrarProdutos(Produtos produtos) 
        {
            if(User.FindFirst("IsAdmin")!.Value != "True")//Checa, através das claims se o usuario é Admin.
                return Unauthorized();

            var WasregisteOrNot = await produtosMethods.CadastrarProdutos(produtos);

            if(WasregisteOrNot)
            return Ok("Product registered.");

            return BadRequest("Some error was ocurred try again.");
        }
       

        [NonAction]private int TakeUserByID()
        {
            var IDUser = Convert.ToInt32(User.FindFirst("id")!.Value);

            return IDUser;
        }
    }
}
