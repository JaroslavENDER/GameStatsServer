using GameStatsServer.Entities;
using GameStatsServer.Models;

namespace GameStatsServer.Extensions
{
    public static class ServerExtensions
    {
        public static ServerInfo CreateServerInfo(this Server server)
        {
            return new ServerInfo
            {
                Name = server.Name,
                GameModes = server.GameModes.Split(", ")
            };
        }

        public static ServerInfoWithEndpoint CreateServerInfoWithEndpoint(this Server server)
        {
            return new ServerInfoWithEndpoint
            {
                Endpoint = server.Endpoint,
                Info = new ServerInfo
                {
                    Name = server.Name,
                    GameModes = server.GameModes.Split(", ")
                }
            };
        }
    }
}
