﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library
{
    public class GameDetail
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string GameApkName { get; set; }
        public string GameName { get; set; }
        public double GamePrice { get; set; }
        public string Description { get; set; }
        [Required]
        public string Publisher { get; set; }
        public bool ChildrenSuitable { get; set; }
        public string AvailableAgeScala { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Rating { get; set; }
        public string LanguageOption { get; set; }
        public bool isApproved { get; set; }

        public ICollection<GenreDetail> Genres { get; set; }
        public List<OrderDetail> Orders { get; set; }
        public PublishRequestDetail Request { get; set; }
        public List<LibraryDetail> Libraries { get; set; }

    }
}
