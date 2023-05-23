using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecenzijeController: ControllerBase 
    {
        private readonly ApplicationDbContext _dbContext;

        public RecenzijeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Recenzije AddUpdate([FromBody] RecenzijaAdd x)
        {
            Recenzije? recenzija;

            if (x.Id == 0)
            {
                recenzija = new Recenzije();
                _dbContext.Add(recenzija);
                recenzija.DatumPostavljanja=DateTime.Now;
            }
            else
            {
                recenzija = _dbContext.Recenzije.Find(x.Id);
                recenzija.DatumModifikacije=DateTime.Now;
            }

            recenzija.Ocjena = x.Ocjena;
            recenzija.Komentar = x.Komentar;
            recenzija.NekretninaId = x.NekretninaId;
            recenzija.KorisnickiNalogId = x.KorisnickiNalogId;

            _dbContext.SaveChanges();
            return recenzija;
        }

        [HttpGet]
        public List<RecenzijaGetAll> GetById(int nekretninaId)
        {

            var data = _dbContext.Recenzije.Where(x => x.NekretninaId == nekretninaId)
                .OrderBy(s => s.Id)
                .Select(s => new RecenzijaGetAll
                {
                    Id= s.Id,
                    Ocjena=s.Ocjena,
                    Komentar=s.Komentar,
                    Korisnik=s.KorisnickiNalog,
                    DatumModifikacije=s.DatumModifikacije,
                    DatumPostavljanja=s.DatumPostavljanja.ToString("dd/MM/yyyy")
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<RecenzijaGetAll> GetByKomentarId(int komentarId)
        {

            var data = _dbContext.Recenzije.Where(x => x.Id == komentarId)
                .OrderBy(s => s.Id)
                .Select(s => new RecenzijaGetAll
                {
                    Id = s.Id,
                    Ocjena = s.Ocjena,
                    Komentar = s.Komentar,
                    Korisnik = s.KorisnickiNalog,
                    DatumModifikacije = s.DatumModifikacije,
                    DatumPostavljanja = s.DatumPostavljanja.ToString("dd/MM/yyyy"),
                    NekretninaId=s.NekretninaId,
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPost("{ID}")]
        public ActionResult Delete(int id)
        {
            Recenzije? recenzija = _dbContext.Recenzije.Find(id);

            if (recenzija == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(recenzija);

            _dbContext.SaveChanges();
            return Ok(recenzija);
        }
    }
}
