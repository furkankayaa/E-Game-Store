using System;
using System.ComponentModel.DataAnnotations;

namespace App.Library
{
    public class GameDetailResponse: GameDetail
    {
        public string CategoryName { get; set; }
    }
}
