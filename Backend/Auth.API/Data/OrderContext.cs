using App.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Data
{
    public class OrderContext : DbContext
    {
        //public OrderContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    var host = Configuration["DBHOST"] ?? "localhost";
        //    var port = Configuration["DBPORT"] ?? "3306";
        //    var pw = Configuration["DBPASSWORD"] ?? "123";

        //    var mysqlConnectionString = $"server={host};userid=root;pwd={pw};" + $"port={port};database=GamesDB";
        //    optionsBuilder.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString), mySqlOptions =>
        //    {
        //        mySqlOptions.EnableRetryOnFailure();
        //    });
        //}

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }


        public DbSet<OrderedGamesDetail> OrderedGamesDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<GameOrderLink> GameOrderLinks { get; set; }
    }
}
