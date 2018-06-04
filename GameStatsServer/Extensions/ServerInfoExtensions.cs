using GameStatsServer.Entities;
using GameStatsServer.Models;

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
                GameModes = string.Join(", ", serverInfo.GameModes)
            };
        }

        public static bool IsValidModel(this ServerInfo serverInfo)
        {
            return serverInfo.Name != null
                && serverInfo.GameModes != null;
        }
    }
}
