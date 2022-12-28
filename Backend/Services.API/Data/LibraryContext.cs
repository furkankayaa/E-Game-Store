using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Library;

namespace Services.API.Data
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options){ }


        public DbSet<LibraryDetail> LibraryDetails { get; set; }
    }
}
