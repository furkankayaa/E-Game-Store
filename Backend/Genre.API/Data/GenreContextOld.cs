using App.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genre.API.Data
{
    public class GenreContextOld : DbContext
    {
        public GenreContextOld(DbContextOptions<GenreContextOld> options) : base(options)
        {
        }

        public DbSet<GenreDetail> GenreDetails { get; set; }
        public DbSet<GameGenreLink> GameGenreLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
