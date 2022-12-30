using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library
{
    public class GenreDetail
    {
        [Key]
        public int GenreID { get; set; }
        public string CategoryName { get; set; }


        //////////
        public ICollection<GameDetail> Games { get; set; }
        //public ICollection<GameGenreLink> Games { get; set; }

    }
}
