using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.Models
{
    public class SignedUser
    {

        [Required(ErrorMessage = "Please input username!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please input password!")]
        public string Password { get; set; }
    }
}
