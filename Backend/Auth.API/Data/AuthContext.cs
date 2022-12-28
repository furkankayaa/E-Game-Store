using App.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Data
{
    public class AuthContext : IdentityDbContext<AppUser>
    {

        public readonly IHttpContextAccessor httpContextAccessor;
        public AuthContext(DbContextOptions<AuthContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public DbSet<AppUserTokens> AppUserTokens { get; set; }

    }
}
