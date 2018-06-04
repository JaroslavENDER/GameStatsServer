using Microsoft.AspNetCore.Http;

namespace GameStatsServer.Services.StatusCodeServices
{
    public class NotFoundService : INotFoundService
    {
        public void Set(HttpContext context)
        {
            context.Response.StatusCode = 404;
        }
    }
}
