using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IUsuario
    {
        public Task<Usuario> SignUp(Usuario usuario);
        public Task<bool> ConfirmUser(string Email, string Senha,Usuario usuario);
        public Task<Usuario> GetUserDtoByEMAIL(string Email);

    }
}
