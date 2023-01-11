using App.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspCoreMvc_App.Models
{
    public class TokenCheckFilter: ActionFilterAttribute
    {
        
            public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var jwt = filterContext.HttpContext.Session.GetString("Token");


            if (string.IsNullOrEmpty(jwt))
            {
                // JWT is not present in the request header, redirect to the login action
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary { { "controller", "UserData" }, { "action", "Index" } });
            }
            else
            {
                // Validate the JWT and continue with the action execution if it is valid
                try
                {
                    var response = Task.Run(() => Helper.isValidAsync(jwt)).Result;
                    if (!response.IsSuccessStatusCode)
                        throw new SecurityTokenException("Invalid JWT");

                   
                }
                catch (SecurityTokenException)
                {
                    // JWT is not valid, redirect to the login action
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary { { "controller", "UserData" }, { "action", "Index" } });
                }
            }
        }
    

        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //}
    }
    
}
