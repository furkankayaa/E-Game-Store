using App.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.API.Data;
//using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly AuthContext _context;
        private readonly IConfiguration _config;

        public OrderController(AuthContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get()
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var purchases = _context.OrderDetails.Where(x => x.UserID == userId)
                .Include(x => x.OrderedGames)
                .ThenInclude(x => x.Genres)
                .Select(x => new
                {
                    x.OrderNum,
                    x.TotalPrice,
                    x.Quantity,
                    x.OrderDate,
                    x.PaymentMethod,
                    OrderedGames = x.OrderedGames.Select(g => new
                    {
                        g.ID,
                        g.ImageUrl,
                        g.GameName,
                        g.GamePrice,
                        g.Description,
                        g.Publisher,
                        g.ChildrenSuitable,
                        g.AvailableAgeScala,
                        g.ReleaseDate,
                        g.Rating,
                        g.LanguageOption,
                        Genres = g.Genres.Select(gen => new
                        {
                            gen.GenreID,
                            gen.CategoryName
                        })
                    })
                })
                .ToList();

            if (purchases != null)
                return Ok(purchases);
            else
                return BadRequest();
        }


        //[FromBody] List<CartItemDetail> cartItems
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> PostAsync()
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //redirectoaction getall cart
            using (var client = new HttpClient())
            {
                AppUser AppUser = _context.Users.FirstOrDefault(x => x.Id == userId);
                var roleId = _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefault();
                var AppRole = _context.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();

                AccessTokenGenerator accessTokenGenerator = new AccessTokenGenerator(_context, _config, AppUser, AppRole);
                AppUserTokens userTokens = accessTokenGenerator.GetToken();

                var token = userTokens.Value;

                // Set the Authorization header of the HTTP request
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                // Send an HTTP GET request to the GetAll action of the CartController
                //HttpResponseMessage response = await client.GetAsync("http://localhost:5000/api/cart/getall");
                HttpResponseMessage response = await client.GetAsync("https://e-gamestore.onrender.com/api/cart/getall");

                // Read the response content as a list of CartItem objects
                List<CartItemDetail> cartItems = await response.Content.ReadAsAsync<List<CartItemDetail>>();

                if (cartItems.Count() != 0)
                {
                    var totalPrice = 0.0;
                    OrderDetail Order = new OrderDetail() { OrderDate = DateTime.Now, Quantity = cartItems.Count, PaymentMethod = "CC", UserID = userId, OrderedGames = { } };

                    var orderedGames = new List<GameDetail>();
                    foreach (var g in cartItems)
                    {

                        var game = _context.GameDetails.Include(x => x.Genres).Where(x => x.ID == g.GameId && x.isApproved == true).FirstOrDefault();
                        if (game != null)
                        {
                            orderedGames.Add(game);

                            totalPrice += game.GamePrice;
                        }

                    }

                    Order.OrderedGames = orderedGames;
                    totalPrice = Math.Round(totalPrice, 2);
                    Order.TotalPrice = totalPrice;

                    //Added to orders table
                    _context.OrderDetails.Add(Order);

                    //Library ye oyunu ekle
                    var userLib = _context.LibraryDetails.Where(x => x.UserId == userId).Include(x => x.Games).FirstOrDefault();
                    if (userLib != null && userLib.Games != null)
                    {

                        userLib.Games.AddRange(orderedGames);
                    }
                    else if(userLib != null){
                        userLib.Games = orderedGames;
                    }
                    else
                    {
                        var libObj = new LibraryDetail { Games = orderedGames, UserId = userId };
                        _context.LibraryDetails.Add(libObj);
                    }


                    // Send an HTTP DELETE request to the DeleteAll action of the CartController
                    //var res = await client.DeleteAsync("http://localhost:5000/api/cart/deleteall");
                    var res = await client.DeleteAsync("https://e-gamestore.onrender.com/api/cart/deleteall");

                    _context.SaveChanges();

                    return Ok();



                    //REDIS PUB/SUB PUBLISH
                    //var redis = ConnectionMultiplexer.Connect("localhost:6379");
                    //var pubsub = redis.GetSubscriber();
                    //pubsub.PublishAsync("order", cartItems[0].UserID);
                }

                else
                {
                    return BadRequest("There is no item in the Cart!");
                }

            }
        }

    }
}
