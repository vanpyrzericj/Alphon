using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;
using MySQL.Data.EntityFrameworkCore.Extensions;
using WebApp.Infrastructure.FeatureFolders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace WebApp
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
            //var sqlConnectionString = Configuration.GetConnectionString("PrimaryConnection");

            //services.AddDbContext<HubContext>(options =>
            //{
            //    options.UseMySQL(sqlConnectionString);
            //});

            services.AddMvc().AddFeatureFolders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Student",
                policy => policy.RequireClaim("role", "Student"));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                policy => policy.RequireClaim("role", "Admin"));
            });

            services.AddAuthentication("MyCookieAuthenticationScheme")
            .AddCookie("MyCookieAuthenticationScheme", options =>
                {
                    options.AccessDeniedPath = "/Forbidden";
                    options.LoginPath = "/SignIn";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}