using GameStatsServer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameStatsServer.DataProviders
{
    public class EFDbContext : DbContext, IDbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<Match> Matches { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync();
        }

        public EFDbContext(DbContextOptions options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder);
    }
}
