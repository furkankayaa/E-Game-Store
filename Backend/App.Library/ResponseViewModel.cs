using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TokenInfo TokenInfo { get; set; }
    }
}
