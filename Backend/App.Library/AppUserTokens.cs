using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class AppUserTokens : IdentityUserToken<string>
    {
        public DateTime ExpireDate { get; set; }
        public string Role { get; set; }
    }
}
