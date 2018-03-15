using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrWebAppTest.Services.Services.Interfaces;
using TrWebAppTest.Services.Services.Logic;
using TrDealsClient.Interfaces;
using TrDealsClient.Logic;

namespace TrWebAppTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWebAppTestService, WebAppTestService>();
            services.AddScoped<IDealClient, DealClient>();

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
