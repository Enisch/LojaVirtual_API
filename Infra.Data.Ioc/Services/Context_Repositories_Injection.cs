using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using Context.ContextForDb;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Ioc.TokenGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsuarioApplication.PasswordHasher;
using UsuarioApplication.Repositories;

namespace Infra.Data.Ioc.Services
{
    public static class Context_Repositories_Injection
    {
        public static IServiceCollection Repositories_Context_Injection(this  IServiceCollection services, IConfiguration configuration)
        {

            var ConnectionString = configuration.GetConnectionString("DefaultString");

            services.AddDbContext<ContextClass>(x => x.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));


            //Repositories
            services.AddScoped<IUsuario,UsuarioRepository>();
            services.AddScoped<IUserDto,DtosUserRepository>();
            services.AddScoped<PasswordHashMethods>();
            services.AddScoped<Token_Generator>();
            services.AddScoped<IProdutos,ProdutosRepository>();

            //AutoMapper
            services.AddAutoMapper(typeof(AutoMapper_Dto));

            return services;
        }

    }
}
