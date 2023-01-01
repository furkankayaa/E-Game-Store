using Microsoft.AspNetCore.Http;
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
        public IFormFile ApkFile { get; set; }
        public IFormFile ImageFile { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string Description { get; set; }
        public bool ChildrenSuitable { get; set; }
        public string AvailableAgeScala { get; set; }
        public string LanguageOption { get; set; }
        public List<int> Genres { get; set; }
    }
}
