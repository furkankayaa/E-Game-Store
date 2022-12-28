using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Cors;
//using App.Library;
using Auth.API.Data;
using Newtonsoft.Json.Linq;

namespace Auth.API.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {

        private readonly ILogger<GamesController> _logger;
        private readonly GameGenreContext _context;

        public GamesController(ILogger<GamesController> logger, GameGenreContext context)
        {
            _logger = logger;
            _context = context;
        }

        //List<GameDetail> toAdd = new List<GameDetail> { new GameDetail { GameName = "Horizon Forbidden West", GamePrice = 524.90, Description = "Süper oyun", Publisher = "Sony Interactive Entertainment Europe" },
        //        new GameDetail { GameName = "Dying Light 2 Stay Human", GamePrice = 314.29, Description = "Süper ötesi oyun", Publisher = "Techland" },
        //        new GameDetail { GameName = "Gran Turismo™ 7", GamePrice = 425.29, Description = "Kötü oyun", Publisher = "Polyphony Digital Inc" },
        //        new GameDetail { GameName = "Sekiro™: Shadows Die Twice", GamePrice = 234.50, Description = "Game of the Year Sürümü", Publisher = "Activision Blizzard" },
        //        new GameDetail { GameName = "FIFA 22", GamePrice = 119.90, Description = "Powered by Football™ sloganıyla yola çıkan EA SPORTS™ FIFA 22, temel oynanış geliştirmeleri ve her moda yenilik katan bir sezon ile oyunu gerçek hayata daha da yakınlaştırıyor.", Publisher = "EA Swiss Sarl" },
        //        new GameDetail { GameName = "Call of Duty®: Vanguard ", GamePrice = 249.50, Description = "Her cephede zafere koş! Pasifik semalarında it dalaşına gir, Fransa'ya paraşütle dalış yap, Stalingrad müdafaasında bir keskin nişancı olarak düşman avla ve Kuzey Afrika'da ilerleyen birliklere katılarak düşmanla çarpış! Call of Duty® oyun serisi seni 2. Dünya Savaşı cephelerine geri götürerek eşi benzeri görülmemiş çapta bir küresel muharebe deneyimi sunan ve Sledgehammer Games tarafından geliştirilen Call of Duty®: Vanguard ile geri dönüyor.", Publisher = "Activision Blizzard" },
        //        new GameDetail { GameName = "Grand Theft Auto Online", GamePrice = 109.50, Description = "GTA yolculuğunuza PS5™ sürümü ile devam ederken GTA Online karakterlerinizi ve ilerlemenizi tek seferde PS5™'e taşıyabilirsiniz.", Publisher = "Rockstar Games" },
        //        new GameDetail { GameName = "Cyberpunk 2077", GamePrice = 239.50, Description = "Cyberpunk 2077, hayatta kalabilmek için ölüm kalım mücadelesi veren bir siberhaydut paralı asker olarak oynadığın, Night City kümekentinde geçen bir açık dünya aksiyon macera RPG'sidir. Yeni nesil güncellemesi ve ücretsiz ek içerikler ile görevlere çıktıkça şöhret kazanıp yeni geliştirmeler açarak karakterini ve oynanış stilini özelleştir. Kurduğun ilişkiler ve aldığın kararlar hikâyeyi ve içinde bulunduğun dünyayı şekillendirecek. Burası, efsanelerin yazıldığı yer. Peki seninki nasıl olacak?", Publisher = "CD PROJEKT" },
        //        new GameDetail { GameName = "Red Dead Redemption 2: Ultimate Edition", GamePrice = 234.15, Description = "Amerika, 1899. Artur Morgan ve Van der Linde çetesi kaçıyor. Federal ajanlar ve ülkenin en iyi ödül avcılarının amansız takibi altında çete üyeleri hayatta kalabilmek için soyguna, yağmaya ve dövüşmeye devam ederek Amerika'nın kalbindeki çetin toprakları geçmek zorunda. Bu süreçte iç çatışmaları da iyice derinleşen çete artık dağılmanın eşiğine gelirken Artur da zor bir seçimle karşı karşıya: onu yetiştiren çeteye sadık mı kalacak yoksa kendi ideallerinin peşinden mi gidecek?", Publisher = "Rockstar Games" },
        //        new GameDetail { GameName = "The Last of Us Part II", GamePrice = 174.50, Description = "Kan donduran bir olay bu huzuru bozduğunda, Ellie adalet arayışında amansız bir yolculuğa çıkar. Bu olayın sorumlularının peşindeyken, yaptıklarının çarpıcı fiziksel ve duygusal sonuçlarıyla yüzleşir.", Publisher = "Sony Interactive Entertainment Europe" }
        //};
        

        //REIMPLEMENT
        [HttpGet]
        [Route("[action]")]
        public GenericResponse<List<GameDetailResponse>> GetAll()
        {
            var games_list = _context.GameDetails.ToList();

            //foreach (var k in toAdd)
            //{
            //    _context.GameDetails.Add(k);
            //}
            //_context.SaveChanges();
            List<GameDetailResponse> allGames = new List<GameDetailResponse>();
            foreach (var i in games_list)
            {
                allGames.Add(GetRequest.GetGameDetailResponse(i));
            }
            GenericResponse<List<GameDetailResponse>> toReturn = new GenericResponse<List<GameDetailResponse>> { Response = allGames, Code=ResponseCode.OK };

            return toReturn;
        }

        //REIMPLEMENT
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<GenericResponse<GameDetailResponse>> GetByIdAsync(int id)
        {
            var game = await _context.GameDetails.FindAsync(id);

            GameDetailResponse myResponse = GetRequest.GetGameDetailResponse(game);
            GenericResponse<GameDetailResponse> toReturn = new GenericResponse<GameDetailResponse> { Response = myResponse, Code = ResponseCode.OK };

            return toReturn;

        }


        //If Admin accepts the publish request posts the game details here
        [HttpPost]
        [Route("[action]")]
        public GenericResponse<GameDetail> Post(GameDetail publishGame)
        {

            //Requestin adminden gelip gelmediğini kontrol et!!

            

            //Oyunu GameDetails tablosuna ekle 
            _context.GameDetails.Add(publishGame);

            //GameGenreLinks tablosuna relationları ekle
            foreach (var genre in publishGame.Genres)
            {
                _context.GameGenreLinks.Add(
                    new GameGenreLink()
                    {
                        Game = publishGame,
                        Genre = genre
                    }
                );
            }


            _context.SaveChanges();
            GenericResponse<GameDetail> toReturn = new GenericResponse<GameDetail> { Response = publishGame, Code = ResponseCode.OK };

            return toReturn;
        }

        //Delete method ekle

    }
}
