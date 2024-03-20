using AutoMapper;
using Context.ContextForDb;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioApplication.PasswordHasher;

namespace UsuarioApplication.Repositories
{
    public class DtosUserRepository : IUserDto
    {
        private readonly IMapper mapper;
        private IUsuario UserMethods;
        private readonly PasswordHashMethods hashMethods;
        private readonly ContextClass context;
        public DtosUserRepository(IMapper mapper, IUsuario userMethods,PasswordHashMethods hashMethods,ContextClass context)
        {
            this.mapper = mapper;
            UserMethods = userMethods;
            this.hashMethods = hashMethods;
            this.context = context;
        }
       
        public async Task<UserDto> SignUpUserDTO(UserDto usuario)//Usuario cadastrado
        {

            byte[] newSenha = Encoding.UTF8.GetBytes(usuario.Password);
            
            if (usuario.Password != null)
            {
                var senhaHash = hashMethods.PasswordHash(usuario.Password);
                newSenha = senhaHash;
            }
            var UserToBeRegistered = mapper.Map<Usuario>(usuario);
            UserToBeRegistered.Senha = newSenha;
            var User = await UserMethods.SignUp(UserToBeRegistered);

            await CreateCONTA(User);
            return mapper.Map<UserDto>(User);
        }

        public async Task CreateCONTA(Usuario usuario)
        {
            ContaUsuario conta = new ContaUsuario
            {   Saldo =10000,//It's for Test Purposes;
                IDUsuario = usuario.idUsuario,
                RankUser = "Iniciante"
            };
            await context.AddAsync(conta);
            await context.SaveChangesAsync();
            
        }

        public async Task<bool> CheckAvailbilityOfEmail(string email)
        {
            
           var UserToCheck = await context.usuarios.Where(x=> x.EmailUsuario ==email).FirstOrDefaultAsync();

            if (UserToCheck != null)
                return true;

            return false;

        }

        
    }
}
