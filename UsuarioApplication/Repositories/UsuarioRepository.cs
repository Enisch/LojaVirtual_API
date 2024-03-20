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
    public class UsuarioRepository : IUsuario
    {
        private readonly ContextClass cc;
        private readonly IMapper mapper;
        private readonly PasswordHashMethods passwordHashMethods;
        public UsuarioRepository(ContextClass cc,IMapper mapper, PasswordHashMethods passwordHashMethods) 
        {
            this.passwordHashMethods = passwordHashMethods;
            this.cc = cc;
            this.mapper = mapper;   
        }
        public async Task<bool> ConfirmUser(string Email, string senha, Usuario usuario)
        {
            var ConfirmEmail = await cc.usuarios.Where(x=> x.EmailUsuario == Email).FirstOrDefaultAsync();

            if (ConfirmEmail == null)
                return false;

            var ConfirmSenha = passwordHashMethods.PasswordCheck(senha, usuario.Senha!);

            if(!ConfirmSenha)
                return false;

            return true;//User is true, therefore, he can do login;    
            
        }

        public async Task<Usuario> GetUserDtoByEMAIL(string Email)
        {
            var User = await cc.usuarios.Where(x => x.EmailUsuario == Email).FirstOrDefaultAsync();
            return User!;
        }

        public Task SignIn()//Login
        {
            return Task.CompletedTask;
        }

        public async Task<Usuario> SignUp(Usuario usuario)
        {
            
            await cc.AddAsync(usuario);
            await cc.SaveChangesAsync();

            return usuario;
        }
    }
}
