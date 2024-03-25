using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Pagination
{
    public class PaginationHeader
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int TotalPageNumber { get; set; }
        public PaginationHeader(int TotalItems,int TotalItemsPerPage,int CurrentPage, int TotalPageNumber) 
        {
            this.TotalPageNumber = TotalPageNumber;
            this.TotalItems = TotalItems;
            this.CurrentPage = CurrentPage;
            this.TotalItemsPerPage = TotalItemsPerPage;
        }
    }
}
