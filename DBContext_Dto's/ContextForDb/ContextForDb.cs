using Microsoft.EntityFrameworkCore;

namespace DBContext_Dto_s.ContextForDb
{
    public class ContextForDb : DbContext
    {
        public ContextForDb(DbContextOptions<ContextForDb> options) : base(options)
        {

        }
    }
}
