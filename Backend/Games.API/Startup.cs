using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
using Games.API.Data;
using Microsoft.EntityFrameworkCore;
using App.Library;

namespace Games.API
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

            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "3306";
            var pw = Configuration["DBPASSWORD"] ?? "123";

            var mysqlConnectionString = $"server={host};userid=root;pwd={pw};" + $"port={port};database=GamesDB";

            services.AddDbContextPool<GameGenreContext>(options =>
            options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure();
            }));


            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Games.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Games.API v1"));
            }



            using (var scope =
                app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<GameGenreContext>();
                dbContext.Database.Migrate();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
