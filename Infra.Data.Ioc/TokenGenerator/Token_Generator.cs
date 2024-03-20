using Infra.Data.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Ioc.TokenGenerator
{
    public class Token_Generator
    {
        private readonly IConfiguration configuration;
        public Token_Generator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string TokenCreation(Usuario usuario)
        {
            
            var TokenDescriptor = new SecurityTokenDescriptor {

                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("id", usuario.idUsuario.ToString()),
                    new Claim("nomeUsuario", usuario.NomeUsuario!),
                    new Claim("EmailUsuario", usuario.EmailUsuario!),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                }),
                Issuer = configuration["JWT_TokenConfig:Issuer"],
                Audience = configuration["JWT_TokenConfig:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT_TokenConfig:SecretKey"]!)),
                SecurityAlgorithms.HmacSha512Signature),
                Expires = DateTime.UtcNow.AddMinutes(10)
            };

            var Handler = new JwtSecurityTokenHandler();
            var TokenCreated = Handler.CreateJwtSecurityToken(TokenDescriptor);
            var TokenString = Handler.WriteToken(TokenCreated);

            return TokenString;
        }
    }
}
