using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{

    public static class GetRequest
    {

        //Returns response string to get request
        public static async Task<HttpResponseMessage> GetApi(string ApiUrl, IHttpContextAccessor contextAccessor)
        {

            //var responseString = "";
            //var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            //request.Method = "GET";
            //request.ContentType = "application/json";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, ApiUrl);

            var token = contextAccessor.HttpContext.Session.GetString("Token");
            request.Headers.Add("Authorization", "Bearer " + token);

            return await client.SendAsync(request);
            

            //using (var response1 = request.GetResponse())
            //{

            //    using (var reader = new StreamReader(response1.GetResponseStream()))
            //    {

            //        responseString = reader.ReadToEnd();
            //    }
            //}
            //return responseString;

        }

        public static async Task<List<PublishRequestDetail>> GetPublishRequestsAsync(IHttpContextAccessor contextAccessor)
        {
            var response = await GetApi("https://e-gamestore.onrender.com/api/publishrequest/getall", contextAccessor);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<PublishRequestDetail>>();
                return data;

            }
            return null;
        }


        //        //Returns GameDetailResponse with category name
        //        public static GameDetailResponse GetGameDetailResponse(GameDetail game)
        //        {

        //            var genres = GetCategories();
        //            //var catName = genres.Where(x => x.GenreID == game.GenreID).FirstOrDefault().CategoryName;

        //            GameDetailResponse myResponse = new GameDetailResponse
        //                {
        //                    ID = game.ID,
        //                Description =game.Description,
        //                GameName = game.GameName,
        //                GamePrice = game.GamePrice,
        //                Publisher = game.Publisher,
        //                ImageUrl = game.ImageUrl,
        //                //GenreID = game.GenreID,
        //                ReleaseDate = game.ReleaseDate,
        //                Rating = game.Rating,
        //                AvailableAgeScala = game.AvailableAgeScala,
        //                ChildrenSuitable = game.ChildrenSuitable,
        //                LanguageOption = game.LanguageOption,
        //                GameApk = game.GameApk,
        //                //CategoryName = catName
        //            };

        //            return myResponse;
        //        }

        //        //Gets List of all GenreDetails
        public static async Task<List<GenreDetail>> GetCategoriesAsync(IHttpContextAccessor contextAccessor)
        {

            var categories = await GetApi($"https://e-gamestore.onrender.com/api/genres/getall", contextAccessor);

            if (categories.IsSuccessStatusCode)
            {
                var data = await categories.Content.ReadFromJsonAsync<List<GenreDetail>>();
                return data;

            }

            return null;
        }

        //        //Gets List of all GameDetails
        //        public static List<GameDetailResponse> GetAllGames()
        //        {
        //            List<GameDetailResponse> games = new List<GameDetailResponse> { };

        //            //While working on Docker container
        //            var gameResponse = GetApi($"http://games.api/api/Games/getall");

        //            //While working on local
        //            //var gameResponse = GetApi($"http://localhost:5000/api/Games/getall");
        //            JObject responseObject = JObject.Parse(gameResponse);

        //            JArray jArray = (JArray)responseObject["response"];
        //            foreach (JObject jObject in jArray)
        //            {

        //                games.Add(new GameDetailResponse { 
        //                    ID= (int)jObject["id"],
        //                    GameName = (string)jObject["gameName"],
        //                    Description = (string)jObject["description"],
        //                    GamePrice = (double)jObject["gamePrice"],
        //                    Publisher = (string)jObject["publisher"],
        //                    ImageUrl = (string)jObject["imageUrl"],
        //                    //GenreID = (int)jObject["genreID"], 
        //                    ChildrenSuitable = (bool)jObject["childrenSuitable"],
        //                    AvailableAgeScala = (string)jObject["availableAgeScala"],
        //                    ReleaseDate = (DateTime)jObject["releaseDate"],
        //                    Rating = (float)jObject["rating"],
        //                    LanguageOption = (string)jObject["languageOption"],
        //                    GameApk = (string)jObject["gameApk"],


        //                    CategoryName = (string)jObject["categoryName"] });
        //            }

        //            return games;
        //        }

        //        public static GameDetailResponse GetGameById(int id)
        //        {
        //            List<GameDetailResponse> games = new List<GameDetailResponse> { };

        //            //While working on Docker container
        //            var gameResponse = GetApi($"http://games.api/api/Games/getbyid/{id}");

        //            //While working on local
        //            //var gameResponse = GetApi($"http://localhost:5000/api/Games/getbyid/{id}");
        //            JObject responseObject = JObject.Parse(gameResponse);

        //            JObject jObject = (JObject)responseObject["response"];


        //            var toReturn = new GameDetailResponse { 
        //                ID= (int)jObject["id"],
        //                GameName = (string)jObject["gameName"],
        //                Description = (string)jObject["description"],
        //                GamePrice = (double)jObject["gamePrice"],
        //                Publisher = (string)jObject["publisher"],
        //                ImageUrl = (string)jObject["imageUrl"],
        //                //GenreID = (int)jObject["genreID"], 
        //                ChildrenSuitable = (bool)jObject["childrenSuitable"],
        //                AvailableAgeScala = (string)jObject["availableAgeScala"],
        //                ReleaseDate = (DateTime)jObject["releaseDate"],
        //                Rating = (float)jObject["rating"],
        //                LanguageOption = (string)jObject["languageOption"],
        //                GameApk = (string)jObject["gameApk"],
        //                CategoryName = (string)jObject["categoryName"] 
        //            };

        //            return toReturn;
        //        }

        //        public static List<CartItemDetail> GetCartItems(string userId)
        //        {
        //            List<CartItemDetail> cartItems = new List<CartItemDetail>();

        //            //While working on Docker container
        //            var cartResponse = GetApi($"http://cart.api/api/cart/getall?userId={userId}");

        //            //While working on local
        //            //var cartResponse = GetApi($"http://localhost:5004/api/cart/getall?userId={userId}");
        //            JObject responseObject = JObject.Parse(cartResponse);

        //            JArray jArray = (JArray)responseObject["response"];
        //            foreach (JObject jObject in jArray)
        //            {
        //                cartItems.Add(new CartItemDetail { ID = (int)jObject["id"], GameName = (string)jObject["gameName"], GamePrice = (double)jObject["gamePrice"], Publisher = (string)jObject["publisher"], ImageUrl = (string)jObject["imageUrl"], UserID= userId });
        //            }

        //            return cartItems;

        //        }

        //        public static List<List<GameOrderLink>> GetPurchases(string userId)
        //        {
        //            List<List<GameOrderLink>> orders = new List<List<GameOrderLink>>();

        //            //While working on Docker container
        //            var orderResponse = GetApi($"http://order.api/api/order/get?userId={userId}");

        //            //While working on locals
        //            //var orderResponse = GetApi($"http://localhost:5006/api/order/get?userId={userId}");
        //            JArray responseObject = JArray.Parse(orderResponse);

        //            foreach (JArray jArray in responseObject)
        //            {
        //                var toAdd = new List<GameOrderLink>() { };

        //                foreach (JObject jObject in jArray)
        //                {
        //                    JObject jGame = (JObject)jObject["game"];
        //                    OrderedGamesDetail game = new OrderedGamesDetail { GameId = (int)jGame["id"], GameName = (string)jGame["gameName"], GamePrice = (double)jGame["gamePrice"], Publisher= (string)jGame["publisher"] };
        //                    JObject jOrder = (JObject)jObject["order"];
        //                    OrderDetail order = new OrderDetail { OrderNum = (int)jOrder["orderNum"], TotalPrice = (double)jOrder["totalPrice"], Quantity = (int)jOrder["quantity"], OrderDate = (DateTime)jOrder["orderDate"], PaymentMethod = (string)jOrder["paymentMethod"], UserID = (string)jOrder["userID"] };

        //                    toAdd.Add(new GameOrderLink { GameId = game.GameId, Game = game, OrderId= order.OrderNum, Order = order });

        //                    //List<OrderedGamesDetail> orderedGames = new List<OrderedGamesDetail>();
        //                    //foreach (JObject game in jGame)
        //                    //{
        //                    //    orderedGames.Add(new OrderedGamesDetail() { GameName = (string)game["gameName"], GamePrice = (double)game["gamePrice"], Publisher = (string)game["publisher"] });
        //                    //}
        //                    //orders.Add(new OrderDetail { OrderNum = (int)jObject["orderNum"], OrderDate = (DateTime)jObject["orderDate"], PaymentMethod = (string)jObject["paymentMethod"], Quantity = (int)jObject["quantity"], TotalPrice = (double)jObject["totalPrice"], UserID = (string)jObject["userId"] });

        //                }
        //                orders.Add(toAdd);
        //            }

        //            return orders;


        //        }
    }
}
