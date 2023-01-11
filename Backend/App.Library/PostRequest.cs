using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public static class PostRequest
    {

        public static async Task<HttpResponseMessage> PostApiAsync(string ApiUrl, IHttpContextAccessor contextAccessor, dynamic d=null)
        {
            
            var json = JsonConvert.SerializeObject(d);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var token = contextAccessor.HttpContext.Session.GetString("Token");


            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (d != null)
            {
                return await client.PostAsync(ApiUrl, data);
            }
            else
            {
                return await client.PostAsync(ApiUrl, null);
            }
        }
        public static async Task<string> DeleteApiAsync(string ApiUrl)
        {
            using var client = new HttpClient();

            var response = await client.DeleteAsync(ApiUrl);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
