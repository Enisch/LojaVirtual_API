using Context.ContextForDb;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
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

        [HttpGet("Buscar Produto:")]
        public async Task<ActionResult> GetProducts(int id,string? nome) 
        {
            if(id >0)
            {
                var ProductsByID = await produtosMethods.GetProdutos(id);
                if(ProductsByID == null)
                    return NotFound("Nenhum item com esse Id.");
                return Ok(ProductsByID);      
            }
            if(nome != null)
            {
                var productsByName = await produtosMethods.GetProdutos(nome);
                if(productsByName == null)
                    return NotFound("Nenhum item com essa nomenclatura.");
                return Ok(productsByName);
            }

            var produtos = await produtosMethods.GetProdutos();
            return Ok(produtos);
        }
        

       

        [NonAction]private int TakeUserByID()
        {
            var IDUser = Convert.ToInt32(User.FindFirst("id")!.Value);

            return IDUser;
        }
    }
}
