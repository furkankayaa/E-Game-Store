using App.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Games.API.Data
{
    public class GamesContextOld : DbContext
    {
        public GamesContextOld(DbContextOptions<GamesContextOld> options) : base(options)
        {
        }

        public DbSet<GameDetail> GameDetails { get; set; }

    }
}
