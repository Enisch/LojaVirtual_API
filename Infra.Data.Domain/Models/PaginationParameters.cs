using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Domain.Models
{
    public class PaginationParameters//Parametros da paginação que o usuario "deverá preencher" sem extrapolar os limites propostos
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(1, 30, ErrorMessage ="O limite de itens por página é 30;")]
        public int PageSize { get; set; }


    }
}
