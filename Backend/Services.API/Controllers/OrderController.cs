using App.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.API.Data;
//using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly OrderContext _context;

        public OrderController(ILogger<OrderController> logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Get([FromQuery] string userId)
        {

            var purchases = _context.OrderDetails.Where(x => x.UserID == userId).ToList();

            List<List<GameOrderLink>> Links = new List<List<GameOrderLink>>();
            foreach (var p in purchases)
            {
                var temp_links = _context.GameOrderLinks.Where(x => x.OrderId == p.OrderNum).ToList();
                foreach (var link in temp_links)
                {
                    link.Order = p;
                    link.Game = _context.OrderedGamesDetails.Find(link.GameId);
                }
                Links.Add(temp_links);

            }
            if (purchases != null)
                return Ok(Links);
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Post([FromBody] List<CartItemDetail> games)
        {

            if (games != null)
            {
                var totalPrice = 0.0;
                //List<OrderedGamesDetail> orderedGames = new List<OrderedGamesDetail>() { };
                OrderDetail Order = new OrderDetail() { OrderDate = DateTime.Now, Quantity = games.Count, PaymentMethod = "CC", UserID = games[0].UserID };

                foreach (var game in games)
                {
                    // Order = Order, OrderId = Order.OrderNum
                    OrderedGamesDetail toAdd = new OrderedGamesDetail() { GameName = game.GameName, GamePrice = game.GamePrice, Publisher = game.Publisher };
                    var isExists = _context.OrderedGamesDetails.Where(x => x.GameName == game.GameName).FirstOrDefault();
                    if (isExists == null)
                    {
                        _context.OrderedGamesDetails.Add(toAdd);
                    }

                    GameOrderLink Link = new GameOrderLink { Game = toAdd, Order = Order };

                    _context.GameOrderLinks.Add(Link);

                    totalPrice += game.GamePrice;
                }
                //Order.OrderedGames = orderedGames;
                totalPrice = Math.Round(totalPrice, 2);
                Order.TotalPrice = totalPrice;

                _context.OrderDetails.Add(Order);

                _context.SaveChanges();

                //REDIS PUB/SUB PUBLISH
                //var redis = ConnectionMultiplexer.Connect("localhost:6379");
                //var pubsub = redis.GetSubscriber();
                //pubsub.PublishAsync("order", games[0].UserID);

                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
