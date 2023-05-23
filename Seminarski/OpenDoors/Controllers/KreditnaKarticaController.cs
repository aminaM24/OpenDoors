using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using System.Text.Json.Serialization.Metadata;

namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class KreditnaKarticaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KreditnaKarticaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public KreditnaKartica Snimi([FromBody] KreditnaKarticaAdd x)
        {

            var kreditnaKartica = new KreditnaKartica
            {
                BrojKartice = x.BrojKartice,
                TipKartice = x.TipKartice,
                KorisnikId = x.KorisnikId,
                CVV = x.CVV,
                datumIsteka=x.datumIsteka,
            };

            _dbContext.Add(kreditnaKartica);
            _dbContext.SaveChanges();
            return kreditnaKartica;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KreditnaKartica
                .OrderBy(s => s.TipKartice)
                .Select(s => new
                {
                    BrojKartice = s.BrojKartice,
                    TipKartice = s.TipKartice,
                    KorisnikId = s.KorisnikId,

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public List<KreditnaKarticaGetAll> GetById(int korisnikId)
        {
            var data = _dbContext.KreditnaKartica.Where(x=>x.KorisnikId==korisnikId)
                .OrderBy(s => s.TipKartice)
                .Select(s => new KreditnaKarticaGetAll
                {
                    BrojKartice = s.BrojKartice,
                    TipKartice = s.TipKartice,
                    datumIsteka=s.datumIsteka,
                    CVV = s.CVV,
                    Id=s.Id,
                })
                .ToList();
            return data;
        }
    }
}
