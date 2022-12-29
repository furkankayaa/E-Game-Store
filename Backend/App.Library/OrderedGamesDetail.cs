using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class OrderedGamesDetail
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string Publisher { get; set; }

        ////Navigation Prop
        //public int OrderId { get; set; }
        //public List<OrderDetail> Orders { get; set; }
        public ICollection<GameOrderLink> Orders { get; set; }

    }
}
