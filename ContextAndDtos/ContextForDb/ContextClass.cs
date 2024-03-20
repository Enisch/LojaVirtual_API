using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Data.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.ContextForDb
{
     public class ContextClass:DbContext
    {
        public ContextClass(DbContextOptions<ContextClass> options):base(options)
        {
            
        }

        public DbSet<Usuario>usuarios { get; set; }
        public DbSet<Produtos>Produtos { get; set; }
        public DbSet<ContaUsuario> contaUsuarios { get; set; }
        public DbSet<Categoria_produtos> categorias { get; set; }
        public DbSet<ControleDeVendas> ControleDeVendas { get; set; }
    }
}
