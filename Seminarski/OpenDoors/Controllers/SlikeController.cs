using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SlikeController : ControllerBase   
    {
        private readonly ApplicationDbContext _dbContext;
        public SlikeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] SlikeGetAll x)
        {
            Slike? slika;
            slika = new Slike
            {
                NekretninaId = x.NekretninaId,
                DatumPostavljanja = DateTime.Now,
            };


            if (x.Slika != "")
            {
                //slika se snima u db
                byte[] slika_bajtovi = x.Slika.ParsirajBase64();
                slika.Slika = slika_bajtovi;
            }

            _dbContext.Add(slika);//priprema sql
            _dbContext.SaveChanges();
            return Ok();
        }



        [HttpGet("{slika_id}")]
        public ActionResult GetSlikaDB(int slika_id)
        {
            var bajtovi_slike = _dbContext.Slike.FirstOrDefault(x => x.Id == slika_id)?.Slika;

            if (bajtovi_slike == null)
            {
                bajtovi_slike = Fajlovi.Ucitaj("wwwroot/images/kuca.png");
            }

            return File(bajtovi_slike, "image/png");
        }

        [HttpGet]
        public ActionResult GetSlikaString(int nekretnina_id)
        {
            var data = _dbContext.Slike.Where(x => x.NekretninaId == nekretnina_id).Select
                (s => new
                {
                    id = s.Id,
                    slikaString = s.Slika.ToBase64()
                }).ToList();

            return Ok(data);
        }

        [HttpPost("{ID}")]
        public ActionResult Delete(int id)
        {
            Slike? slika = _dbContext.Slike.Find(id);

            if (slika == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(slika);

            _dbContext.SaveChanges();
            return Ok(slika);
        }

    }
}
