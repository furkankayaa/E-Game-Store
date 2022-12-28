using Microsoft.EntityFrameworkCore;
using App.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class PublishRequestContext: DbContext
    {
        public PublishRequestContext(DbContextOptions<PublishRequestContext> options) : base(options)
        {
        }

        public DbSet<PublishRequestDetail> PublishRequestDetails { get; set; }
    }
}
