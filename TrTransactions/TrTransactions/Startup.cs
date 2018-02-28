using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using TrTransactions.Data;
using TrTransactions.Data.DataSeeders.Interfaces;
using TrTransactions.Data.DataSeeders.Logic;
using TrTransactions.Data.Infrastructure.Interfaces;
using TrTransactions.Data.Infrastructure.Logic;
using TrTransactions.Data.Repositories.Interfaces;
using TrTransactions.Data.Repositories.Logic;
using TrTransactions.Service.Services.Interfaces;
using TrTransactions.Service.Services.Logic;

namespace TrTransactions
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

            services.AddScoped<ITransactionService, TransactionService>();

            services.AddMvc();

            // Добавление документации
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("transaction", new Info { Title = "Управление транзакциями", Version = "transaction" });

                var xmlPath = Path.Combine(_env.ContentRootPath, "TrTransactions.xml");
                c.IncludeXmlComments(xmlPath);
                c.IgnoreObsoleteProperties();
            });

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

            // Подключение документации
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ShowJsonEditor();
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/transaction/swagger.json", "transaction");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
