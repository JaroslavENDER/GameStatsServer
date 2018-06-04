using Microsoft.AspNetCore.Http;

namespace GameStatsServer.Services.StatusCodeServices
{
    public interface IStatusCodeService
    {
        void Set(HttpContext context);
    }
}
