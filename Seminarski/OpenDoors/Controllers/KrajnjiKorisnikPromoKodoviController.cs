using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class KrajnjiKorisnikPromoKodoviController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KrajnjiKorisnikPromoKodoviController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public KrajnjiKorisnikPromoKodovi Snimi([FromBody] KrajnjiKorisnikPromoKodoviAdd x)
        {

            var krajnjiKorisnikPromoKodovi = new KrajnjiKorisnikPromoKodovi
            {
               KrajnjiKorisnikId=x.KrajnjiKorisnikId,
               PromoKodoviId=x.PromoKodoviId
            };

            _dbContext.Add(krajnjiKorisnikPromoKodovi);
            _dbContext.SaveChanges();
            return krajnjiKorisnikPromoKodovi;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KrajnjiKorisnikPromoKodovi
                .OrderBy(s => s.KrajnjiKorisnikId)
                .Select(s => new
                {
                    KrajnjiKorisnikId = s.KrajnjiKorisnikId,
                    PromoKodoviId = s.PromoKodoviId

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



