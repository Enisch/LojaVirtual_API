using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Pagination
{
    //Essa classe tem o proposito de oferecer o recurso de realizar as buscas no banco de dados por páginas sem sobrecarregar a aplicação.
    //Apesar de posuírem poucas informações neste projeto este é recurso sempre muito útil de se aplicar/entender.
    public class PagedList<T>: List<T>
    {
        //Essa classe aceita um tipo generico <T>
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        public PagedList(IEnumerable<T> Items, int PageNumber,int pageSize,int Count)
        {
            CurrentPage = PageNumber;
            PageSize = pageSize;
            TotalCount = Count;
            TotalPage = (int) Math.Ceiling(TotalCount/(double) PageSize);

            AddRange(Items);
        }
    }
}
