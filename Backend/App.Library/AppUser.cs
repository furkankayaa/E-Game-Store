using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class AppUser : IdentityUser
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public override string Id { get; set; }

        [Required]
        [DisplayName("Ad Soyad")]
        [StringLength(50)]
        public string FullName { get; set; }
    }
}
