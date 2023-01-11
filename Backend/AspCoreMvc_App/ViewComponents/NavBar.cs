using App.Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMvc_App.ViewComponents
{
    public class NavBar:ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            ViewBag.isLoggedIn = false;
            if (HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            {
                ViewBag.isLoggedIn = true;
            }
            return View();
        }
        
    }
}
