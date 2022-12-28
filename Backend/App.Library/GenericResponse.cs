using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class GenericResponse<T>
    {
        public T Response { get; set; }
        public ResponseCode Code { get; set; }
    }
}
