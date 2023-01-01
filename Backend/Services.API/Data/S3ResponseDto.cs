using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.API.Data
{
    public class S3ResponseDto
    {
        public S3Object Object { get; set; }
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "";
    }
}
