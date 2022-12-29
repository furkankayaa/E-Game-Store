using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class OrderDetail
    {
        [Key]
        public int OrderNum { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public string UserID { get; set; }

        //Navigation Prop
        //public List<OrderedGamesDetail> OrderedGames { get; set; }
        public ICollection<GameOrderLink> OrderedGames { get; set; }

    }
}
