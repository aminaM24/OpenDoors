using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TipNekretnineController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TipNekretnineController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public TipNekretnine Snimi([FromBody] TipNekretnineAdd x)
        {

            var tipNekretnine = new TipNekretnine
            {
               Tip=x.Tip,
               Opis=x.Opis,
            };

            _dbContext.Add(tipNekretnine);
            _dbContext.SaveChanges();
            return tipNekretnine;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.TipNekretnine
                .OrderBy(s => s.Tip)
                .Select(s => new TipNekretnineGetAll
                {
                    Id=s.Id,
                    Tip = s.Tip,

                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}


