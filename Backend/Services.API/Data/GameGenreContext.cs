using App.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class GameGenreContext : DbContext
    {
        public GameGenreContext(DbContextOptions<GameGenreContext> options) : base(options)
        {
        }

        public DbSet<GameDetail> GameDetails { get; set; }
        public DbSet<GenreDetail> GenreDetails { get; set; }
        public DbSet<GameGenreLink> GameGenreLinks { get; set; }
    }
}
