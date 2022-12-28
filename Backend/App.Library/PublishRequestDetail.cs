using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class PublishRequestDetail 
    {
        public int ID { get; set; }
        public GameDetail Game { get; set; }
        public DateTime RequestDate { get; set; }
        public string UserId { get; set; }
    }
}
