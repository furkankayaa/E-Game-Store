using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Authorization;
using App.Library;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AspCoreMvc_App
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();

            //services.AddDbContext<StudentDetailContext>();
            //services.AddIdentity<AppUser, AppRole>(x =>
            //{
            //    x.Password.RequireUppercase = false;
            //    x.Password.RequireNonAlphanumeric = false;
            //    x.Password.RequiredLength = 2;
            //}).AddEntityFrameworkStores<StudentDetailContext>();

            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                        .RequireAuthenticatedUser()
            //                        .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));

            //});
            //services.ConfigureApplicationCookie(options => { options.LoginPath = "/UserData";
            //}) ;
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(options =>
            //        {
            //            options.LoginPath = "/UserData/";
            //            //options.LogoutPath = "/UserData/Index/";
            //        });

            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IDbContextOptions, DbContextOptions>();
            
            //var mysqlConnectionString = Configuration.GetConnectionString("mysqlConnectionString");

            //var host = Configuration["DBHOST"] ?? "localhost";
            //var port = Configuration["DBPORT"] ?? "3306";
            //var pw = Configuration["DBPASSWORD"] ?? "123";

            //var mysqlConnectionString = $"server={host};userid=root;pwd={pw};" + $"port={port};database=Student-DB";

            //services.AddDbContext<StudentDetailContext>(options =>
            //    options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString), mySqlOptions =>
            //    {
            //        mySqlOptions.EnableRetryOnFailure();
            //    }),
            //    contextLifetime: ServiceLifetime.Transient,
            //    optionsLifetime: ServiceLifetime.Singleton
            //) ;


            //services.AddDbContextPool<StudentDetailContext>(options =>
            //{
            //    options.UseMySql($"server={host};userid=root;pwd={pw};"
            //      + $"port={port};database=Student-DB");
            //});

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            //using (var scope =
            //    app.ApplicationServices.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetService<StudentDetailContext>();
            //    dbContext.Database.Migrate();
            //}


            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=UserData}/{action=Index}/");
            });
        }
    }
}
