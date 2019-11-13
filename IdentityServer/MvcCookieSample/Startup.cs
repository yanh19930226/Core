using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MvcCookieSample.Data;
using MvcCookieSample.Models;
using Microsoft.AspNetCore.Identity;
using IdentityServer4;
using MvcCookieSample.Services;
using IdentityServer4.Services;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace MvcCookieSample
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
            //添加DbContext(用户数据库)
            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]); });
            //动态客户端和资源使用EFcore(IdentityServer数据库)
            const string connectionString = @"Server=.;Database=IdentityServer4;Trusted_Connection=True;MultipleActiveResultSets=true";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //添加Identity
            services.AddIdentity<ApplicationUser, ApplicationUserRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //修改默认严格密码模式
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequiredLength = 10;
            });
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //测试使用内存
                //.AddInMemoryClients(Config.GetClients())
                //.AddInMemoryApiResources(Config.GetResource())
                //.AddInMemoryIdentityResources(Config.GetIdentityResource())
                .AddConfigurationStore(options=> {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                })
                .AddOperationalStore(options => {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                })
                //正式数据库
                .AddAspNetIdentity<ApplicationUser>()
                .Services.AddScoped<IProfileService, ProfileService>();
            //测试阶段使用
            //.AddTestUsers(Config.GetTestUsers());
            services.AddScoped<ConsentServices>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            InitIdentityServerDatabase(app);
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void InitIdentityServerDatabase(IApplicationBuilder app)
        {
            using (var scope=app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var configurationDbContext=scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                if (!configurationDbContext.Clients.Any())
                {
                    foreach (var item in Config.GetClients())
                    {
                        configurationDbContext.Clients.Add(item.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.ApiResources.Any())
                {
                    foreach (var item in Config.GetResource())
                    {
                        configurationDbContext.ApiResources.Add(item.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
                if (!configurationDbContext.IdentityResources.Any())
                {
                    foreach (var item in Config.GetIdentityResource())
                    {
                        configurationDbContext.IdentityResources.Add(item.ToEntity());
                    }
                    configurationDbContext.SaveChanges();
                }
            }
        }
    }
}
