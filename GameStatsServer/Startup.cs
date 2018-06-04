using GameStatsServer.Extensions;
using GameStatsServer.Services;
using GameStatsServer.Services.StatusCodeServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStatsServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEFDbContext(Configuration.GetConnectionString("DefaultConnection"));

            services.AddTransient<INormalizeReportsCount, NormalizeReportsCount>();
            services.AddTransient<IBadRequestService, BadRequestService>();
            services.AddTransient<INotFoundService, NotFoundService>();

            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
