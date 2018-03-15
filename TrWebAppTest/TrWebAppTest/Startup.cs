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
            services.AddSingleton<IWebAppTestService, WebAppTestService>();
            services.AddSingleton<IDealClient, DealClient>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }
    }
}
