using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TrTransactions.Data;
using TrTransactions.Data.DataSeeders.Interfaces;
using TrTransactions.Data.DataSeeders.Logic;
using TrTransactions.Data.Infrastructure.Interfaces;
using TrTransactions.Data.Infrastructure.Logic;
using TrTransactions.Data.Repositories.Interfaces;
using TrTransactions.Data.Repositories.Logic;

namespace TrTransactions
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Подключение БД
            var dbConnectionString = _configuration["DbConnectionString"] ?? _configuration.GetConnectionString("DefaultConnection");
            
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TrTransactionsContext>(options =>
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

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDataSeeder, DataSeeder>();

            return services.BuildServiceProvider();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Применение миграций
            var dbContext = app.ApplicationServices.GetService<TrTransactionsContext>();
            dbContext.Database.Migrate();

            // Заполнение справочников
            var dataSeeder = app.ApplicationServices.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedCurrencies().GetAwaiter().GetResult();
        }
    }
}
