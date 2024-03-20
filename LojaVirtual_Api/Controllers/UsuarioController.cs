using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Infra.Data.Ioc.TokenGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual_Api.Controllers
{
    [ApiController]
    [Route("Api/Usuario/(Controller)")]
    public class UsuarioController : Controller
    {
        private readonly IUserDto userDto;
        private readonly IUsuario userMethods;
        private readonly Token_Generator token;
        public UsuarioController(IUserDto userDto,Token_Generator token,IUsuario userMethods)
        {
            this.token = token;
            this.userDto = userDto;
            this.userMethods = userMethods;
        }
        [HttpPost("Sign Up")]
        public async Task<ActionResult> UserRegistration(UserDto user)
        {
           var CheckIFEmailISAvailble = await userDto.CheckAvailbilityOfEmail(user.EmailUsuario!);

            if (CheckIFEmailISAvailble)
                return BadRequest("Digite outro endereço de Email.");

            var NewUser = await userDto.SignUpUserDTO(user);
            if (NewUser!=null)
                return Ok("User registered sucessfuly.");

            return BadRequest("Something is wrong.");  
        }

        [HttpPost("Sign In")]
        public async Task<ActionResult<TokenS>> UserLogin(LogginUsuario Login)
        {
            var user = await userMethods.GetUserDtoByEMAIL(Login.EmailUser!);

            if (user == null) return Unauthorized("E-mail incorreto.");

            var IStrueORNot = await userMethods.ConfirmUser(user.EmailUsuario!,Login.PAsswordUser!, user);

            if (!IStrueORNot)
                return Unauthorized("Usuario inexistente.");

            var tokenC = token.TokenCreation(user);

            return new TokenS
            { Token = tokenC};

        }
        [Authorize]
        [HttpGet]
        public ActionResult<string> TesteDeAuthorize()
        {
            return Ok("Deu certo!");
        }
    }
}
