using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class PublishRequestDetail 
    {
        public int ID { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserId { get; set; }

        public int GameId { get; set; }
        public GameDetail Game { get; set; }
    }
}
