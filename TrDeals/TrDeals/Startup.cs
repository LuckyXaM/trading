using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrDeals.Data;
using TrDeals.Data.DataSeeders.Interfaces;
using TrDeals.Data.DataSeeders.Logic;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Infrastructure.Logic;
using TrDeals.Data.Repositories.Interfaces;
using TrDeals.Data.Repositories.Logic;

namespace TrDeals
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Подключение БД
            var dbConnectionString = _configuration["DbConnectionString"] ?? _configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TrDealsContext>(options =>
                {
                    options.UseNpgsql(dbConnectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorCodesToAdd: null);
                        });
                }, ServiceLifetime.Scoped);
            services.AddSingleton<IDataSeeder, DataSeeder>();
            services.AddScoped<IDealRepository, DealRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services.BuildServiceProvider();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Применение миграций
            var dbContext = app.ApplicationServices.GetService<TrDealsContext>();
            dbContext.Database.Migrate();

            // Заполнение справочников
            var dataSeeder = app.ApplicationServices.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedCurrencies().GetAwaiter().GetResult();
        }
    }
}
