using App.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublishRequest.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishRequestControl : ControllerBase
    {

        private readonly ILogger<PublishRequestControl> _logger;
        private readonly PublishRequestContext _context;

        public PublishRequestControl( ILogger<PublishRequestControl> logger, PublishRequestContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Route("[action]")]
        public List<PublishRequestDetail> GetAll()
        {
            var request_list = _context.PublishRequestDetails.ToList();

            List<PublishRequestDetail> toReturn = request_list;
            return toReturn;
        }

        [HttpGet]
        [Route("[action]")]
        public GenericResponse<PublishRequestDetail> GetById(int id)
        {
            var request = _context.PublishRequestDetails.Find(id);
            GenericResponse<PublishRequestDetail> toReturn = new GenericResponse<PublishRequestDetail> { Response = request, Code = ResponseCode.OK }; 
            return toReturn;
        }

        [HttpPost]
        [Route("[action]")]
        public GenericResponse<PublishRequestDetail> Post(GameAndGenres data)
        {
            data.Game.Genres = data.Genres;

            PublishRequestDetail gameRequest = new PublishRequestDetail { Game = data.Game, RequestDate = DateTime.Now, UserId = data.UserId };
            _context.PublishRequestDetails.Add(gameRequest);
            _context.SaveChanges();

            GenericResponse<PublishRequestDetail> toReturn = new GenericResponse<PublishRequestDetail> { Response = gameRequest, Code = ResponseCode.OK };

            return toReturn;
        }


        [HttpDelete]
        [Route("[action]")]
        public GenericResponse<PublishRequestDetail> Delete(int id)
        {
            var toDelete = _context.PublishRequestDetails.Find(id);
            _context.PublishRequestDetails.Remove(toDelete);
            _context.SaveChanges();

            var toReturn = new GenericResponse<PublishRequestDetail> { Response = toDelete, Code = ResponseCode.OK };
            return toReturn;
        }
        
    }
}
