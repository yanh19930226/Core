using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MultiDataMigrate.Data;

namespace MultiDataMigrate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region MySql

            services.AddDbContext<MigrationDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MySqlConnection"), b => b.MigrationsAssembly("MultiDataMigrate"));
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("MultiDataMigrate", new OpenApiInfo { Title = "MultiDataMigrate", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region SwaggerUI
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/MultiDataMigrate/swagger.json", "MultiDataMigrate"); });

            #endregion
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
