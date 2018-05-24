using GameStatsServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GameStatsServer.DataProviders
{
    public interface IDbContext : IDisposable
    {
        DbSet<Server> Servers { get; }
        DbSet<Match> Matches { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
