using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class CartItemDetail
    {
        public int ID { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string ImageUrl { get; set; }
        public string Publisher { get; set; }
        public string UserID { get; set; }
    }
}
