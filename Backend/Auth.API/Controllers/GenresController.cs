using App.Library;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {

        private readonly ILogger<GenresController> _logger;
        private readonly GameGenreContext _context;

        public GenresController(ILogger<GenresController> logger, GameGenreContext context)
        {
            _logger = logger;
            _context = context;
        }

        //List<GenreDetail> toAdd = new List<GenreDetail> {
        //    new GenreDetail { CategoryName = "Spor" },
        //    new GenreDetail { CategoryName = "Macera" },
        //    new GenreDetail { CategoryName = "Simülasyon" },
        //    new GenreDetail { CategoryName = "FPS" }
        //};

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll()
        {   
            //foreach (var k in toAdd)
            //{
            //    _context.GenreDetails.Add(k);
            //}
            //_context.SaveChanges();
            var found = _context.GenreDetails.ToList();
            return Ok(found);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            
            var found = await _context.GenreDetails.FindAsync(id);
            return Ok(found.CategoryName);
        }
    }
}
