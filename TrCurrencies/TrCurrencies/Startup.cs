using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using TrCurrencies.Data;
using TrCurrencies.Data.DataSeeders.Interfaces;
using TrCurrencies.Data.DataSeeders.Logic;
using TrCurrencies.Data.Infrastructure.Interfaces;
using TrCurrencies.Data.Infrastructure.Logic;
using TrCurrencies.Data.Mappings.Configurations;
using TrCurrencies.Data.Repositories.Interfaces;
using TrCurrencies.Data.Repositories.Logic;
using TrCurrencies.Service.Services.Interfaces;
using TrCurrencies.Service.Services.Logic;

namespace TrCurrencies
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
                .AddDbContext<TrCurrenciesContext>(options =>
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
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICurrencyService, CurrencyService>();

            // Добавление документации
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("currency", new Info { Title = "Отслеживание валют", Version = "currency" });

                var xmlPath = Path.Combine(_env.ContentRootPath, "TrCurrencies.xml");
                c.IncludeXmlComments(xmlPath);
                c.IgnoreObsoleteProperties();
            });

            // Добавление автомаппера
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                AutoMapperConfiguration.Configure();
            });
            var mapper = config.CreateMapper();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Применение миграций
            var dbContext = app.ApplicationServices.GetService<TrCurrenciesContext>();
            dbContext.Database.Migrate();

            // Заполнение справочников
            var dataSeeder = app.ApplicationServices.GetRequiredService<IDataSeeder>();
            dataSeeder.SeedDataAsync().GetAwaiter().GetResult();

            // Подключение документации
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ShowJsonEditor();
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/currency/swagger.json", "currency");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
