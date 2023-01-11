using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public static class Helper
    {
        public static bool isPurchased(GameDetailResponse game, List<List<GameOrderLink>> p)
        {
            foreach (var i in p)
            {
                foreach (var j in i)
                {
                    if (j.Game.GameName == game.GameName)
                        return true;
                }
            }
            return false;
        }

        public static bool isCartItem(GameDetail game, List<CartItemDetail> cart)
        {
            if (cart.Where(x => x.GameName == game.GameName).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }

        public static async Task<HttpResponseMessage> isValidAsync(string token)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://e-gamestore.onrender.com/api/auth/");

            // add the token to the request header
            request.Headers.Add("Authorization", "Bearer " + token);

            // send the request and handle the response
            var response = await client.SendAsync(request);
            return response;
        }
        
    }
}
