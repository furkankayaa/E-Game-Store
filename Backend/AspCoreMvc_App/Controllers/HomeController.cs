using App.Library;
using AspCoreMvc_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspCoreMvc_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContext;
        //private readonly StudentDetailContext _context;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _httpContext = httpContext;
        }


        [TokenCheckFilter]
        [HttpGet]
        [Route("")]
        [Route("[action]")]
        public async Task<IActionResult> IndexAsync(string searchTerm)
        {
            //var allCategories = await GetRequest.GetCategoriesAsync(_httpContext);
            //ViewBag.category = allCategories;
            if (searchTerm != null)
            {
                ViewBag.category = searchTerm;
                return View();
            }
            else
            {
                return View();
            }
            //var token = HttpContext.Session.GetString("Token");
            //if (string.IsNullOrEmpty(token))
            //{
            //    return RedirectToAction("Index", "UserData");
            //}

            //var response = await Helper.isValidAsync(token);

            //if (response.IsSuccessStatusCode)
            //{
            //return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "UserData");
            //}
        }
        
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string searchTerm)
        {
            var response = await GetRequest.GetApi($"https://e-gamestore.onrender.com/api/publishrequest/search?searchTerm={searchTerm}", _httpContext);
            List<PublishRequestDetail> data = null;
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadFromJsonAsync<List<PublishRequestDetail>>();
                
            }
            return RedirectToAction("Index", data);

        }
        
        public async Task<IActionResult> Approve(int requestId)
        {
           
            await PostRequest.PostApiAsync($"https://e-gamestore.onrender.com/api/publishrequest/approve?requestId={requestId}", _httpContext);
            //var response = await PostRequest.PostApiAsync("http://localhost:5000/api/publishrequest/approve?requestId={requestId}", postData, _httpContext);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Reject(int requestId)
        {
           
            await PostRequest.PostApiAsync($"https://e-gamestore.onrender.com/api/publishrequest/reject?requestId={requestId}", _httpContext);
            //var response = await PostRequest.PostApiAsync("http://localhost:5000/api/publishrequest/reject?requestId={requestId}", postData, _httpContext);

            return RedirectToAction("Index");
        }
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("[action]/{categoryName}")]
        //public IActionResult Index(string categoryName)
        //{

        //    var allCategories = GetRequest.GetCategories();
        //    var category = allCategories.Where(x => x.CategoryName == categoryName).FirstOrDefault();
        //    ViewBag.category = category;
        //    return View(allCategories);
        //}




        //[Route("[action]")]
        //public async Task<IActionResult> CartAsync([FromQuery(Name = "gameId")] int gameId)
        //{
        //    string userId = User.Identity.Name;
        //    var cartItems = GetRequest.GetCartItems(userId);
        //    var purchased = GetRequest.GetPurchases(userId);

        //    //if (!HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Identity.Application"))
        //    //{
        //    //string returnUrl = HttpContext.Request.Query["ReturnUrl"].ToString();
        //    //return Redirect(returnUrl);
        //    //return RedirectToAction("Index", "UserData", new { redirectUrl = redirectUrl, redirectCtrl = redirectCtrl, gameId = gameId });
        //    //    return View();
        //    //}
        //    //else
        //    //{
        //    ViewBag.totalPrice = 0.00;
        //    if (gameId != 0)
        //    {
        //        //While working on Docker container
        //        var url = "http://cart.api/api/cart/post";

        //        //While working on local
        //        //var url = "http://localhost:5004/api/cart/post";


        //        var game = GetRequest.GetGameById(gameId);
        //        if (!Helper.isCartItem(game, cartItems) && !Helper.isPurchased(game, purchased))
        //        {
        //            CartItemDetail gameToAdd = new CartItemDetail
        //            {
        //                GameName = game.GameName,
        //                GamePrice = game.GamePrice,
        //                ImageUrl = game.ImageUrl,
        //                Publisher = game.Publisher,
        //                UserID = userId
        //            };
        //            await PostRequest.PostApiAsync(url, gameToAdd);
        //            cartItems = GetRequest.GetCartItems(userId);
        //        }

        //        foreach (var item in cartItems)
        //        {
        //            ViewBag.totalPrice += item.GamePrice;
        //        }
        //        if (cartItems != null)
        //            ViewBag.totalPrice = Math.Round(ViewBag.totalPrice, 2);
        //        return View(cartItems);
        //    }
        //    else
        //    {
        //        foreach (var item in cartItems)
        //        {
        //            ViewBag.totalPrice += item.GamePrice;
        //        }
        //        if (cartItems != null)
        //            ViewBag.totalPrice = Math.Round(ViewBag.totalPrice, 2);
        //        return View(cartItems);
        //    }
        //    //}

        //}

        //[Route("[action]")]
        //public async Task<IActionResult> CartRemoveAsync(int id)
        //{
        //    var userId = User.Identity.Name;
        //    //While working on Docker container
        //    var url = $"http://cart.api/api/cart/delete?id={id}&userId={userId}";

        //    //While working on local
        //    //var url = $"http://localhost:5004/api/cart/delete?id={id}&userId={userId}";


        //    await PostRequest.DeleteApiAsync(url);

        //    return RedirectToAction("Cart");
        //}

        //public async Task<IActionResult> CartRemoveAllAsync(string user)
        //{
        //    var userId = User.Identity.Name;

        //    //While working on Docker container
        //    var url = $"http://cart.api/api/cart/delete?userId={userId}";

        //    //While working on local
        //    //var url = $"http://localhost:5004/api/cart/delete?userId={userId}";

        //    if (user == userId)
        //        await PostRequest.DeleteApiAsync(url);

        //    return RedirectToAction("Purchases");
        //}


        //[Route("[action]")]
        //public IActionResult Purchases()
        //{
        //    string userId = User.Identity.Name;

        //    var purchased = GetRequest.GetPurchases(userId);
        //    return View(purchased);
        //}

        //[Route("[action]")]
        //public async Task<IActionResult> CheckoutAsync()
        //{
        //    string userId = User.Identity.Name;
        //    var cartItems = GetRequest.GetCartItems(userId);

        //    //While working on Docker container
        //    var url = "http://order.api/api/order/post";

        //    //While working on local
        //    //var url = "http://localhost:5006/api/order/post";

        //    await PostRequest.PostApiAsync(url, cartItems);

        //    return RedirectToAction("Purchases");
        //}




        //var redis = ConnectionMultiplexer.Connect("localhost:6379");
        //var pubsub = redis.GetSubscriber();

        //pubsub.Subscribe("order", (channel, message) => Console.WriteLine("ALINDI"));
    }
}
