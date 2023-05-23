using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class PogodnostiNekretnineController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PogodnostiNekretnineController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public PogodnostiNekretnine Snimi([FromBody] PogodnostiNekretnineAdd x)
        {

            var pogodnostiNekretnine = new PogodnostiNekretnine
            {
               Naziv=x.Naziv,
               Opis=x.Opis

            };

            _dbContext.Add(pogodnostiNekretnine);
            _dbContext.SaveChanges();
            return pogodnostiNekretnine;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.PogodnostiNekretnine
                .OrderBy(s => s.Naziv)
                .Select(s => new PogodnostiNekretnineGetAll()
                {
                    Id=s.Id,
                    Naziv = s.Naziv                    

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}


