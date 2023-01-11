using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.Models
{
    public class UserData
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please input username!")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please input password!")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Does not match!")]
        public string ConfirmPassword { get; set; }


    }
}
