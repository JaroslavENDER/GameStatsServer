using GameStatsServer.DataProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameStatsServer.Extensions
{
    public static class AddEFDbContextExtension
    {
        public static void AddEFDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDbContext, EFDbContext>();
        }
    }
}
