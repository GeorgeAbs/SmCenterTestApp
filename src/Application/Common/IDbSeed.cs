using Microsoft.EntityFrameworkCore;

namespace SmCenterTestApp.Application.Common
{
    public interface IDbSeed<TDbContext> where TDbContext : DbContext
    {
        public Task SeedAsync(TDbContext context);
    }
}
