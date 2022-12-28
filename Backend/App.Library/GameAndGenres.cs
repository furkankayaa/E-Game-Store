﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameAndGenres
    {
        public GameDetail Game { get; set; }
        public List<GenreDetail> Genres { get; set; }
        public string UserId { get; set; }
    }
}
