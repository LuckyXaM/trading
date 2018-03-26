using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrWebAppTest.Services.Services.Interfaces;
using TrWebAppTest.Services.Services.Logic;
using TrDealsClient.Interfaces;
using TrDealsClient.Logic;
using TrTransactionClient.Interfaces;
using TrTransactionClient.Logic;
using TrCurrencyClient.Logic;
using TrCurrencyClient.Interfaces;

namespace TrWebAppTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWebAppTestService, WebAppTestService>();
            services.AddSingleton<IDealClient, DealClient>();
            services.AddSingleton<ITransactionClient, TransactionClient>();
            services.AddSingleton<ICurrencyClient, CurrencyClient>();

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
