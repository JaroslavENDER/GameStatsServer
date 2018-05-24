using GameStatsServer.Entities;
using GameStatsServer.Models;
using System.Linq;

namespace GameStatsServer.Extensions
{
    public static class ServerInfoExtensions
    {
        public static Server CreateServer(this ServerInfo serverInfo, string endpoint)
        {
            return new Server
            {
                Endpoint = endpoint,
                Name = serverInfo.Name,
                GameModes = serverInfo.GameModes.ToList()
            };
        }
    }
}
