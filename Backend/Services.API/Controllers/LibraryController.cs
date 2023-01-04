using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly IConfiguration _config; 
        public LibraryController(AuthContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var libraryItems = _context.LibraryDetails.Where(x => x.UserId == userId)
                .Include(x => x.Games)
                .ThenInclude(x => x.Genres)
                .Select(x => new
                {
                    Games = x.Games.Select(g => new
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

            return Ok(libraryItems);
        }



        private async Task<Stream> DownloadFileAsync(string objName, string bucketName)
        {
            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var service = new StorageService();
            var result = await service.DownloadFileAsync(cred, objName, bucketName);
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Download(int id)
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var gameToDownload = _context.GameDetails.Find(id);
            var isInOrders = _context.OrderDetails.Any(x => x.UserID == userId && x.OrderedGames.Contains(gameToDownload));

            if (isInOrders)
            {
                // Set the bucket name and object name
                string bucketName = _config["AwsConfiguration:PrivateBucket"];
                string objName = gameToDownload.GameApkName;

                // Download the file from S3
                using (var stream = await DownloadFileAsync(objName, bucketName))
                {
                    // Set the content type and file name
                    string contentType = "application/vnd.android.package-archive";
                    string fileName = $"{gameToDownload.GameName}.apk";
                            
                    // Return the file for download
                    return File(stream, contentType, fileName);
                }

            }

            return BadRequest("You don't own the game!");
        }
    }
}
