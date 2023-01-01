using Amazon.S3;
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
        
        public PublishRequestController( ILogger<PublishRequestController> logger, AuthContext context, IConfiguration config)
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


        private async Task<S3Object> UploadFileAsync(IFormFile file)
        {
            await using var memoryStr = new MemoryStream();
            await file.CopyToAsync(memoryStr);

            var fileExt = Path.GetExtension(file.Name);
            var objName = $"{Guid.NewGuid()}.{fileExt}";

            var s3Obj = new S3Object()
            {
                BucketName = _config["AwsConfiguration:BucketName"],
                InputStream = memoryStr,
                Name = objName
            };

            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AWSAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var service = new StorageService();
            var result = await service.UploadFileAsync(s3Obj, cred);

            return result.Object;
        }

        //private async Task<byte[]> DownloadFileAsync(string file)
        //{
        //    await using var memoryStr = new MemoryStream();

        //    try
        //    {
                
        //    }
        //}

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<GenericResponse<PublishRequestDetail>> PostAsync(GameAndGenres data)
        {
            var userId = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            //var service = new GoogleDriveService(CreateDriveService());
            //var apkFile = service.UploadFile(data.Game.ApkName, "application/vnd.android.package-archive", data.Game.GameApkBin);
            //var imgFile = service.UploadFile(data.Game.ImageName, data.Game.ImageMimeType, data.Game.ImageBin);

            var apkFile = data.Game.ApkFile;
            var imageFile = data.Game.ImageFile;

            var apkObj = await UploadFileAsync(apkFile);
            var imageObj = await UploadFileAsync(imageFile);

            var game = new GameDetail
            {
                AvailableAgeScala = data.Game.AvailableAgeScala,
                ChildrenSuitable = data.Game.ChildrenSuitable,
                Description = data.Game.Description,
                GameName = data.Game.GameName,
                GamePrice = data.Game.GamePrice,
                GameApk = apkObj.Name,
                ImageUrl = imageObj.Name,
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

            return toReturn;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public ActionResult Approve(int requestId)
        {
            var request = _context.PublishRequestDetails.Find(requestId);
            var approve_game = _context.GameDetails.Find(request.GameId);
            approve_game.isApproved = true;

            Delete(requestId);

            _context.SaveChanges();
            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public ActionResult Reject (int requestId)
        {
            var request = _context.PublishRequestDetails.Find(requestId);
            var del_game = _context.GameDetails.Find(request.GameId);
            _context.GameDetails.Remove(del_game);

            Delete(requestId);

            _context.SaveChanges();
            return Ok();
        }


        ////Sadece admin yapacak
        //[Authorize(Roles = "Admin")]
        //[HttpDelete]
        //[Route("[action]")]
        private void Delete(int id)
        {

            //var role = _context.httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            //Console.WriteLine(role);
            var toDelete = _context.PublishRequestDetails.Find(id);
            if (toDelete != null)
            {
                _context.PublishRequestDetails.Remove(toDelete);
                _context.SaveChanges();

                //return new GenericResponse<PublishRequestDetail> { Response = toDelete, Code = ResponseCode.OK };

            }

            //var toReturn = new GenericResponse<PublishRequestDetail> { Response = toDelete, Code = ResponseCode.BadRequest };
            //return toReturn;
        }
        
    }
}
