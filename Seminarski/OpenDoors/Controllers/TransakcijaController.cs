using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransakcijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TransakcijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public Transakcija Snimi([FromBody] TransakcijaAdd x)
        {

            var transakcija = new Transakcija
            {
                Iznos=x.Iznos,
                Popust=x.Popust,
                NacinPlacanja=x.NacinPlacanja,
                NekretninaId=x.NekretninaId,
                KorisnikId=x.KorisnikId,
                KreditnaKarticaId=x.KreditnaKarticaId,
                RezervacijaId=x.RezervacijaId,
            };

            _dbContext.Add(transakcija);
            _dbContext.SaveChanges();
            return transakcija;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Transakcija
                .OrderBy(s => s.Iznos)
                .Select(s => new
                {
                    Iznos = s.Iznos,
                    Popust = s.Popust,
                    NacinPlacanja = s.NacinPlacanja,
                    NekretninaId = s.NekretninaId,
                    KorisnikId = s.KorisnikId,
                    KreditnaKarticaId = s.KreditnaKarticaId,
                    RezervacijaId = s.RezervacijaId,

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}


