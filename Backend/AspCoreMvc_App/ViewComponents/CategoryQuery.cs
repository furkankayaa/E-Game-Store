using App.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AspCoreMvc_App.ViewComponents
{
    public class CategoryQuery : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string searchTerm)
        {
            HttpContextAccessor contextAccessor = new HttpContextAccessor();
            var allRequests = await GetRequest.GetPublishRequestsAsync(contextAccessor);


            var response = await GetRequest.GetApi($"https://e-gamestore.onrender.com/api/publishrequest/search?searchTerm={searchTerm}", contextAccessor);
            List<PublishRequestDetail> data = null;
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadFromJsonAsync<List<PublishRequestDetail>>();

            }

            if (searchTerm == null)
            {
                return View(allRequests);
            }
            else
            {
                //var categoryGames = allRequests.Where(x => x.Game.Genres.Any(g => g.CategoryName == category.CategoryName)).ToList();
                return View(data);
            }

        }

        public IViewComponentResult Approve()
        {
            int id = Convert.ToInt32(ViewComponentContext.ViewContext.RouteData.Values["id"]);
            // Use the id parameter here
            return View();
        }
    }
}
