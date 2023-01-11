using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Library;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AspCoreMvc_App.Models
{
    public class StudentDetailContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public StudentDetailContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var host = Configuration["DBHOST"] ?? "localhost";
            //var port = Configuration["DBPORT"] ?? "3306";
            //var pw = Configuration["DBPASSWORD"] ?? "123";

            //var mysqlConnectionString = $"server={host};userid=root;pwd={pw};" + $"port={port};database=Student-DB";
            //optionsBuilder.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString), mySqlOptions =>
            //{
            //    mySqlOptions.EnableRetryOnFailure();
            //});
        }
        //public StudentDetailContext(DbContextOptions<StudentDetailContext> options) : base(options)
        //{
        //}
        //public DbSet<StudentDetail> StudentDetails { get; set; }
        //public DbSet<UserData> UserDatas { get; set; }
        //Table name StudentDetails
    }
}
