using Infra.Data.Domain.Pagination;
using System.Text.Json;

namespace LojaVirtual_Api.Extensions
{
    public static class HttpExtensions
    {
        public static void  AddPaginationHeader(this HttpResponse response,PaginationHeader header)
        {
            var JsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(header, JsonOptions));
            response.Headers.Append("Acess-Control-Expose-Headers", "pagination");
        }
    }
}
