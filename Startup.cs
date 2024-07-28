using LembreteApp.Interfaces;
using LembreteApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;

namespace LembreteApp
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
            services.AddControllers();

            // Registrar o IMongoClient utilizando uma string de conexão direta para o MongoDB local
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var connectionString = "mongodb://localhost:27017";
                Console.WriteLine($"Connection String: {connectionString}"); // Linha de depuração
                return new MongoClient(connectionString);
            });

            services.AddSingleton<LembreteService>();
            
            // Adicionar esta linha para registrar ILembreteService
            services.AddScoped<ILembreteService, LembreteService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
