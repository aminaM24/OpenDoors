using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NekretninaPogodnostiNekretnineController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NekretninaPogodnostiNekretnineController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public NekretninaPogodnostiNekretnine Snimi([FromBody] NekretninaPogodnostiNekretnineAdd x)
        {

            var nekretninaPogodnostiNekretnine = new NekretninaPogodnostiNekretnine
            {
               NekretninaId=x.NekretninaId,
               PogodnostiNekretnineId=x.PogodnostiNekretnineId
            };

            _dbContext.Add(nekretninaPogodnostiNekretnine);
            _dbContext.SaveChanges();
            return nekretninaPogodnostiNekretnine;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.NekretninaPogodnostiNekretnine
                .OrderBy(s => s.NekretninaId)
                .Select(s => new
                {
                    NekretninaId = s.NekretninaId,
                    PogodnostiNekretnineId = s.PogodnostiNekretnineId
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
    }
}



