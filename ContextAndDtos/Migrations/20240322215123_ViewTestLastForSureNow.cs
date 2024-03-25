using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class ViewTestLastForSureNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            var command = @"create or alter view ProdutosCategoria_View as
                    select Produtos.idProdutos,Produtos.NomeProduto,Produtos.ValorProduto,Produtos.QtdProduto,categoria_Produtos.NomeCategoria
                    from produtos 
                    inner join categoria_Produtos 
                    on categoria_Produtos.IdCategoria = Produtos.IdCategoria";
            //order by  categoria_Produtos.NomeCategoria, Produtos.NomeProduto;
            migrationBuilder.Sql(command);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var command = @"DROP VIEW ProdutosCategoria_View;";
            migrationBuilder.Sql(command);
        }
    }
}