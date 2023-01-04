using App.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AuthContext _context;
        public CartController(AuthContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult GetAll()
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var cartItems = _context.CartItemDetails.ToList();
            var cartItems = _context.CartItemDetails.Where(x => x.UserID == userId)
                .Select(x => new
                {
                    x.ID,
                    x.GameId,
                    x.GameName,
                    x.GamePrice,
                    x.ImageUrl,
                    x.Publisher
                })
                .ToList();

            return Ok(cartItems);
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Post(int gameId)
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var g = _context.GameDetails.Where(x => x.ID == gameId && x.isApproved == true).FirstOrDefault();
            var libGames = _context.LibraryDetails.Where(x => x.UserId == userId).FirstOrDefault().Games;

            //if g isn't null and is not in the users library or there is no user library
            if (g != null && (libGames != null && !libGames.Contains(g) || libGames == null) )
            {
                CartItemDetail addItem = new CartItemDetail { GameId= g.ID, GameName = g.GameName, GamePrice = g.GamePrice, ImageUrl = g.ImageUrl, Publisher = g.Publisher, UserID = userId };

                var Exists = _context.CartItemDetails.Where(x => x.GameId == addItem.GameId && x.UserID == addItem.UserID).FirstOrDefault();
                if (Exists == null)
                {
                    _context.CartItemDetails.Add(addItem);
                    _context.SaveChanges();

                    return Ok();
                }

            }
            
            return BadRequest();
            

        }


        [HttpDelete("[action]")]
        public ActionResult Delete(int id)
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDelete = _context.CartItemDetails.Find(id);
            if (toDelete?.UserID == userId)
            {
                _context.CartItemDetails.Remove(toDelete);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest(); 


        }

        [HttpDelete("[action]")]
        public ActionResult DeleteAll()
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDelete = _context.CartItemDetails.Where(x => x.UserID == userId).ToList();

            if (toDelete.Count() != 0)
            {
                _context.CartItemDetails.RemoveRange(toDelete);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest("No cart items to delete!");

        }
    }
}
