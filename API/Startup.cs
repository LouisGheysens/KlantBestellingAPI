using BusinessLayer.Interfaces;
using BusinessLayer.Managers;
using DataLayer;
using DataLayer.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        //private readonly string connectionString = @"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=WebAPI;Integrated Security=True"; //Origineel database
        private readonly string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=TestDb;Integrated Security=True"; //Test Database

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //services.AddControllers(setup => setup.ReturnHttpNotAcceptable = true).AddNewtonsoftJson(); //oud
            services.AddControllers(); //toegevoegd  //nieuw
            services.AddSingleton<IKlantRepository>(x => new KlantRepository(connectionString));
            services.AddSingleton<IBestellingRepository>(x => new BestellingRepository(connectionString));
            services.AddSingleton<KlantManager>();
            services.AddSingleton<BestellingManager>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
