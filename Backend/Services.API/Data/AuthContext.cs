using App.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class AuthContext : IdentityDbContext<AppUser>
    {

        public readonly IHttpContextAccessor httpContextAccessor;

        //public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        //{
        //}

        public AuthContext(DbContextOptions<AuthContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }


        public DbSet<AppUserTokens> AppUserTokens { get; set; }


        //Cart items dbset
        public DbSet<CartItemDetail> CartItemDetails { get; set; }


        //GameGenreContext
        public DbSet<GameDetail> GameDetails { get; set; }
        public DbSet<GenreDetail> GenreDetails { get; set; }
        //public DbSet<GameGenreLink> GameGenreLinks { get; set; }

        //OrderContext
        //public DbSet<OrderedGamesDetail> OrderedGamesDetails { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<GameOrderLink> GameOrderLinks { get; set; }

        //PublishRequestContext
        public DbSet<PublishRequestDetail> PublishRequestDetails { get; set; }
        
        
        //LibraryContext
        public DbSet<LibraryDetail> LibraryDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GameGenreLink>()
            //    .HasKey(link => new { link.GameId, link.GenreId });
            //modelBuilder.Entity<GameGenreLink>()
            //    .HasOne(link => link.Game)
            //    .WithMany(link => link.Genres)
            //    .HasForeignKey(link => link.GameId);
            //modelBuilder.Entity<GameGenreLink>()
            //   .HasOne(link => link.Genre)
            //   .WithMany(link => link.Games)
            //   .HasForeignKey(link => link.GenreId);


            //modelBuilder.Entity<GameOrderLink>()
            //    .HasKey(link => new { link.GameId, link.OrderId });
            //modelBuilder.Entity<GameOrderLink>()
            //    .HasOne(link => link.Game)
            //    .WithMany(link => link.Orders)
            //    .HasForeignKey(link => link.GameId);
            //modelBuilder.Entity<GameOrderLink>()
            //   .HasOne(link => link.Order)
            //   .WithMany(link => link.OrderedGames)
            //   .HasForeignKey(link => link.OrderId);

            //modelBuilder.Entity<PublishRequestDetail>()
            //    .HasOne(l => l.Game )
            //    .WithOne(l => l.Request)
            //    .HasForeignKey(l => l.)

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(login => new { login.LoginProvider, login.ProviderKey, login.UserId});

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(login => new { login.UserId, login.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(login => new { login.UserId, login.LoginProvider, login.Name });

                
            // Other model configuration goes here...
        }
    }
}
