﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TrCurrencyClient.Interfaces;
using TrCurrencyClient.Logic;
using TrDeals.Data;
using TrDeals.Data.Infrastructure.Interfaces;
using TrDeals.Data.Infrastructure.Logic;
using TrDeals.Data.Repositories.Interfaces;
using TrDeals.Data.Repositories.Logic;
using TrDeals.Service.Mapping.Configurations;
using TrDeals.Service.Services.Interfaces;
using TrDeals.Service.Services.Logic;
using TrTransactionClient.Interfaces;
using TrTransactionClient.Logic;

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
            // Настройки доступа к api
            services.AddCors(opt =>
            {
                opt.AddPolicy("Corspolicy",
                    builder => builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials());
            });

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
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ICurrencyClient, CurrencyClient>();
            services.AddSingleton<ITransactionClient, TransactionClient>();
            services.AddScoped<IDealService, DealService>();

            // Добавление документации
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("deal", new Info { Title = "Предложения", Version = "deal" });

                var xmlPath = Path.Combine(_env.ContentRootPath, "TrDeals.xml");
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
            // Настройки доступа к api
            app.UseCors("Corspolicy");

            //Применение миграций
            var dbContext = app.ApplicationServices.GetService<TrDealsContext>();
            dbContext.Database.Migrate();

            // Подключение документации
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ShowJsonEditor();
                c.RoutePrefix = "api-docs";
                c.SwaggerEndpoint("/swagger/deal/swagger.json", "deal");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
