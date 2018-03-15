using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrWebAppTest.Services.Services.Interfaces;
using TrWebAppTest.Services.Services.Logic;

namespace TrWebAppTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWebAppTestService, WebAppTestService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
        }
    }
}
