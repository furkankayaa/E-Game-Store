using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameProps
    {
        public string ImageUrl { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string Description { get; set; }
        
        [Required]
        public string Publisher { get; set; } //GamePublishers tablosu yap (düşük öncelik)
        //public string PublisherId { get; set; } 

        public bool ChildrenSuitable { get; set; }
        public string AvailableAgeScala { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Rating { get; set; }
        public string LanguageOption { get; set; }
        public string GameApk { get; set; }
        public bool isApproved { get; set; }
    }
}
