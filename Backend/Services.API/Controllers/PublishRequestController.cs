using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using App.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.API.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PublishRequestController : ControllerBase
    {

        private readonly ILogger<PublishRequestController> _logger;
        private readonly AuthContext _context;
        private readonly IConfiguration _config;

        public PublishRequestController(ILogger<PublishRequestController> logger, AuthContext context, IConfiguration config)
        {
            _logger = logger;
            _context = context;
            _config = config;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("[action]")]
        public List<PublishRequestDetail> GetAll()
        {
            var request_list = _context.PublishRequestDetails.Select(x => x)
                .Include(x => x.Game).ToList();

            foreach (var req in request_list)
            {
                var genres = _context.GenreDetails.Where(x => x.Games.Contains(req.Game)).ToList();
                req.Game.Genres = genres;
            }

            List<PublishRequestDetail> toReturn = request_list;
            return toReturn;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("[action]")]
        public GenericResponse<PublishRequestDetail> GetById(int id)
        {
            //    var request = _context.PublishRequestDetails.Include(x => x.Game)
            //        .ThenInclude(x => x.Genres).Where(r => r.ID == id).FirstOrDefault();


            var req = _context.PublishRequestDetails.Find(id);
            if (req != null)
            {
                req.Game = _context.GameDetails.Where(x => x.ID == req.GameId).FirstOrDefault();
                var genres = _context.GenreDetails.Where(x => x.Games.Contains(req.Game)).ToList();
                req.Game.Genres = genres;
            }

            //var game = _context.GameDetails.Include(x => x.Genres);

            //var game = _context.GameDetails.Where(x => x.ID == request.GameId).FirstOrDefault();
            //var g = _context.GameDetails.Where(x => x.ID == request.GameId).SelectMany(c => c.Genres).ToList();
            //request.Game = game;
            GenericResponse<PublishRequestDetail> toReturn = new GenericResponse<PublishRequestDetail> {
                Response = req, Code = ResponseCode.OK };
            return toReturn;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("[action]")]
        public IActionResult SearchRequest(string searchTerm)
        {
            var requests = _context.PublishRequestDetails
                .Include(x => x.Game)
                .Where(g => g.Game.GameName.Contains(searchTerm) && g.Game.isApproved == false)
                .ToList();

            foreach (var r in requests)
            {
                r.Game.Genres = _context.GenreDetails.Where(x => x.Games.Contains(r.Game)).ToList();
            }
            return Ok(requests);
        }


        private async Task DeleteFileAsync(string objName, string bucketName)
        {
            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var service = new StorageService();
            await service.DeleteFileAsync(cred, objName, bucketName);
        }

        private async Task<string> UploadFileAsync(IFormFile file, string objName, string bucketName, bool makePublic)
        {
            await using var memoryStr = new MemoryStream();
            await file.CopyToAsync(memoryStr);


            var s3Obj = new Data.S3Object()
            {
                BucketName = bucketName,
                InputStream = memoryStr,
                Name = objName
            };

            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
                //AwsKey = _config["AWSAccessKey"],
                //AwsSecretKey = _config["AWSSecretKey"]
            };

            var service = new StorageService();
            var objUrl = await service.UploadFileAsync(s3Obj, cred, makePublic);

            return objUrl;
        }


        [Authorize]
        [HttpPost]
        [Route("[action]")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(400_000_000)]
        public async Task<GenericResponse<PublishRequestDetail>> PostAsync([FromForm] GameProps a)
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //var game_converted = new GameDetail
            //{
            //    AvailableAgeScala = a.AvailableAgeScala,
            //    ChildrenSuitable = Convert.ToBoolean(a.ChildrenSuitable),
            //    Description = a.Description,
            //    GameName = a.GameName,
            //    GamePrice = Convert.ToDouble(a.GamePrice),
            //    LanguageOption = a.LanguageOption
            //};
            GameAndGenres data= new GameAndGenres { Game = a , GenreIds = new List<int> { Convert.ToInt32(a.Genres) } };

            if (a != null)
            {
                var apkFile = a.ApkFile;
                var imageFile = a.ImageFile;
                var fileExt = Path.GetExtension(apkFile.Name);
                var apkNewName = $"{Guid.NewGuid()}.{fileExt}";

                var imgFileExt = Path.GetExtension(imageFile.Name);
                var imgNewName = $"{Guid.NewGuid()}.{imgFileExt}";

                var privateBucket = _config["AwsConfiguration:PrivateBucket"];
                var publicBucket = _config["AwsConfiguration:PublicBucket"];

                Console.WriteLine(_config["AwsConfiguration:AWSAccessKey"]);

                var apkObjUrl = UploadFileAsync(apkFile, apkNewName, privateBucket, false);
                var imgObjUrl = UploadFileAsync(imageFile, imgNewName, publicBucket, true);

                var game = new GameDetail
                {
                    AvailableAgeScala = data.Game.AvailableAgeScala,
                    ChildrenSuitable = Convert.ToBoolean(data.Game.ChildrenSuitable),
                    Description = data.Game.Description,
                    GameName = data.Game.GameName,
                    GamePrice = Convert.ToDouble(data.Game.GamePrice),
                    GameApkName = apkNewName,
                    ImageName = imgNewName,
                    ImageUrl = await imgObjUrl,
                    LanguageOption = data.Game.LanguageOption,
                    Publisher = userName,
                    Rating = 0,
                    isApproved = false
                };


                PublishRequestDetail gameRequest = new PublishRequestDetail
                {
                    UserId = userId,
                    RequestDate = DateTime.Now
                };

                gameRequest.Game = game;

                var genres = _context.GenreDetails.Where(x => data.GenreIds.Contains(x.GenreID)).ToList();

                gameRequest.Game.Genres = genres;

                _context.PublishRequestDetails.Add(gameRequest);
                _context.SaveChanges();

                GenericResponse<PublishRequestDetail> toReturn = new GenericResponse<PublishRequestDetail> { Response = gameRequest, Code = ResponseCode.OK };
                await apkObjUrl;

                return toReturn;
            }
            GenericResponse<PublishRequestDetail> Error = new GenericResponse<PublishRequestDetail> { Response = null, Code = ResponseCode.BadRequest };
            return Error;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public ActionResult Approve([FromQuery]int requestId)
        {
            var request = _context.PublishRequestDetails.Find(requestId);
            if (request != null)
            {
                var approve_game = _context.GameDetails.Find(request.GameId);
                approve_game.isApproved = true;

                Delete(requestId);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RejectAsync (int requestId)
        {
            var request = _context.PublishRequestDetails.Find(requestId);
            var del_game = _context.GameDetails.Find(request.GameId);

            if (del_game != null)
            {
                //Delete Image from db
                //await DeleteFileAsync(del_game.ImageName, _config["AwsConfiguration:PublicBucket"]);
                //Delete Apk from db
                //await DeleteFileAsync(del_game.GameApkName, _config["AwsConfiguration:PrivateBucket"]);

                _context.GameDetails.Remove(del_game);
            }
            else
            {
                Delete(requestId);
                _context.SaveChanges();

                return BadRequest("No game found, Request is deleted anyways.");
            }

            Delete(requestId);
            _context.SaveChanges();

            return Ok();
        }

        private void Delete(int id)
        {

            var toDelete = _context.PublishRequestDetails.Find(id);
            if (toDelete != null)
            {
                _context.PublishRequestDetails.Remove(toDelete);
                _context.SaveChanges();
            }
        }
    }
}
