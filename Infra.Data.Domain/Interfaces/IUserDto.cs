using Infra.Data.Domain;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Interfaces
{
    public interface IUserDto
    {
        public Task<UserDto> SignUpUserDTO(UserDto usuario);
        public Task CreateCONTA(Usuario usuario);
        public Task<bool> CheckAvailbilityOfEmail(string email);

        
    }
}
