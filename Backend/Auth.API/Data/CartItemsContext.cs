using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Library;


namespace Cart.API.Data
{
    public class CartItemsContext: DbContext
    {
        public CartItemsContext(DbContextOptions<CartItemsContext> options) : base(options)
        {
        }

        public DbSet<CartItemDetail> CartItemDetails { get; set; }
    }
}
   