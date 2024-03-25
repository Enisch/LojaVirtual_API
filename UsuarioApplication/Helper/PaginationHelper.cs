using Infra.Data.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuarioApplication.Helper
{
    public static class PaginationHelper
    {
        //Essa classe vai realizar as configurações do modelo PagedList do Infra.Data.Domain
        public static async Task<PagedList<T>> CreateList<T>(IQueryable<T> query, int pageSize, int pageNumber)where T : class
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();//Esse é o cálculo dos items por página

            /*Funcionamento do cálculo:
             await query.Skip((1-1) * 5)Essa função,com os valores propostos, significa que não serão pulados nenhum item e que,
            .Take(5)Essa função, pode retornar os 5 primeiros itens.


            Caso esstejamos na 2 página, await query.Skip((2-1) * 5) a função pulará os 5 primeiros itens e
            .Take(5) retornará os 5 primeiros itens após o Skip.*/

            return new PagedList<T>(items, pageNumber, pageSize, count);//Retorna a classe fornecendo os valores esperados pelo construtor;
        }


    }
}
