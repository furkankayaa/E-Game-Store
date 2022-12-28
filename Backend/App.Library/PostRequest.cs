using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public static class PostRequest
    {

        public static async Task<string> PostApiAsync<T>(string ApiUrl, T d)
        {

            var json = JsonConvert.SerializeObject(d);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            using var client = new HttpClient();

            var response = await client.PostAsync(ApiUrl, data);

            var result = await response.Content.ReadAsStringAsync();

            return result;
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
