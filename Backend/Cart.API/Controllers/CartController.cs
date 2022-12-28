using App.Library;
using Cart.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartItemsContext _context;
        public CartController(CartItemsContext context)
        {
            _context = context;
        }

        [Route("[action]")]
        public GenericResponse<List<CartItemDetail>> GetAll(string userId)
        {

            //var cartItems = _context.CartItemDetails.ToList();
            var cartItems = _context.CartItemDetails.ToList().Where(x => x.UserID == userId).ToList();

            GenericResponse<List<CartItemDetail>> toReturn = new GenericResponse<List<CartItemDetail>>() { Response = cartItems, Code = ResponseCode.OK };

            return toReturn;
        }

        [Route("[action]")]
        [HttpPost]
        public GenericResponse<CartItemDetail> Post([FromBody] CartItemDetail game)
        {
            CartItemDetail addItem = new CartItemDetail {GameName = game.GameName, GamePrice = game.GamePrice, ImageUrl = game.ImageUrl, Publisher = game.Publisher, UserID = game.UserID };

            var Exists = _context.CartItemDetails.Where(x => x.GameName == addItem.GameName && x.UserID == addItem.UserID).FirstOrDefault();
            if (Exists == null)
            {
                _context.CartItemDetails.Add(addItem);
                _context.SaveChanges();
                GenericResponse<CartItemDetail> toReturn = new GenericResponse<CartItemDetail> { Response = addItem, Code = ResponseCode.OK };
                return toReturn;
            }
            else
            {
                return new GenericResponse<CartItemDetail> { Response = null, Code = ResponseCode.BadRequest };
            }

            
        }


        [HttpDelete("[action]")]
        public GenericResponse<CartItemDetail> Delete(int id, string userId)
        {
            var toDelete = _context.CartItemDetails.Find(id);
            if (toDelete.UserID == userId)
            {
                _context.CartItemDetails.Remove(toDelete);
                _context.SaveChanges();

                GenericResponse<CartItemDetail> toReturn = new GenericResponse<CartItemDetail>() { Response = toDelete, Code = ResponseCode.OK };
                return toReturn;
            }

            return new GenericResponse<CartItemDetail>() { Response = null, Code = ResponseCode.BadRequest }; 


        }

        [HttpDelete("[action]")]
        public GenericResponse<List<CartItemDetail>> DeleteAll(string userId)
        {
            var toDelete = _context.CartItemDetails.Where(x => x.UserID == userId).ToList();

            foreach (var item in toDelete)
            {
                _context.CartItemDetails.Remove(item);
                _context.SaveChanges();
            }

            GenericResponse<List<CartItemDetail>> toReturn = new GenericResponse<List<CartItemDetail>> { Response = toDelete, Code = ResponseCode.OK };

            return toReturn;
        }
    }
}
