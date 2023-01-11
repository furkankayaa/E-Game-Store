using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using App.Library;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AspCoreMvc_App.Controllers
{
    public class UserDataController : Controller
    {

        //private readonly UserManager<AppUser> _userManager;
        //private readonly SignInManager<AppUser> _signInManager;

        public UserDataController()
        {
            //_userManager = userManager;
            //_signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("Login/[action]")]
        public async Task<IActionResult> IndexAsync()
        {
            //if (!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
            var token = HttpContext.Session.GetString("Token");

            var response = await Helper.isValidAsync(token);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ReturnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        [AllowAnonymous]
        [HttpPost]
        //string uname, string pwd
        public async Task<IActionResult> Verify(SignedUser p)
        {
            string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();

            if (ModelState.IsValid)
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://e-gamestore.onrender.com/api/Auth/AdminLogin");
                //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5000/api/Auth/AdminLogin");

                var json = new
                {
                    email = p.Username,
                    password = p.Password
                };

                request.Content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

                // send the request and handle the response
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    // read the response body as a string
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // deserialize the string into a AdminLoginResponse object
                    ResponseViewModel loginResponse = JsonConvert.DeserializeObject<ResponseViewModel>(responseContent);

                    // extract the token from the response object
                    string token = loginResponse.TokenInfo.Token;

                    // store the token in a session variable
                    HttpContext.Session.SetString("Token", token);

                    // handle success
                    if (returnUrl != "")
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //Just redirect to our index after logging in. 
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //return View("Index");
                }
                //var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
                //if (result.Succeeded)
                //{
                //    //if (redirectUrl != "" && redirectCtrl != "")
                //    if(returnUrl != "")
                //    {
                //        return Redirect(returnUrl);
                //    }
                //    else
                //    {
                //        //Just redirect to our index after logging in. 
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
            }
            return View("Index");
        }


        //[AllowAnonymous]
        //[HttpPost]
        //[Route("[controller]/[action]")]
        ////string name, string pw, string cnf_pw
        //public async Task<IActionResult> CreateAsync(UserData p)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //AppUser user = new AppUser()
        //        //{
        //        //    UserName = p.Username
        //        //};
        //        ////var result = await _userManager.CreateAsync(user, p.Password);

        //        //if (result.Succeeded)
        //        //{
        //        //    return RedirectToAction("Index", "Home");
        //        //}
        //        //else
        //        //{
        //        //    foreach (var item in result.Errors)
        //        //    {
        //        //        ModelState.AddModelError("", item.Description);
        //        //    }
        //        //}
        //    }
        //    return View("Index");

        //}

        public async Task<IActionResult> Log_Out()
        {

            //await _signInManager.SignOutAsync();

            HttpContext.Session.Remove("Token");
            Console.WriteLine(HttpContext.Session.GetString("Token"));
            //return RedirectToAction("Index", "Home");
            return View("Index");
        }

    }
}
