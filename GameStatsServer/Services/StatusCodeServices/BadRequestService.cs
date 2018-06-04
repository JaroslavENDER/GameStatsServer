using Microsoft.AspNetCore.Http;

namespace GameStatsServer.Services.StatusCodeServices
{
    public class BadRequestService : IBadRequestService
    {
        public void Set(HttpContext context)
        {
            context.Response.StatusCode = 400;
        }
    }
}
