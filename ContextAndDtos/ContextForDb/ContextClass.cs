using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Data.Domain.Models;
using Infra.Data.Domain.Views;
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
        public DbSet<ProdutosComCategoria_View> ProdutosCategoria_View { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //É preciso instalar o pacote Nu-get EF-Core Relational para usar o metodo ToView();
            modelBuilder.Entity<ProdutosComCategoria_View>().HasNoKey().ToView("ProdutosCategoria_View");

            modelBuilder.Entity<ProdutosComCategoria_View>(x =>

            x.ToSqlQuery("Select * from lojavirtual.produtoscategoria_view;")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
