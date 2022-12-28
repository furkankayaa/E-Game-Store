using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameGenreLink
    {
        [Key]
        public int GameId { get; set; }
        public GameDetail Game { get; set; }
        [Key]
        public int GenreId { get; set; }
        public GenreDetail Genre { get; set; }

        //public string UserId { get; set; }
    }
}
