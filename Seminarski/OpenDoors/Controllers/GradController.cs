using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GradController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public Grad Snimi([FromBody] GradAdd x)
        {

            var grad = new Grad
            {
                Naziv = x.Naziv,
                PostanskiBroj = x.PostanskiBroj
            };

            _dbContext.Add(grad);
            _dbContext.SaveChanges();
            return grad;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Grad
                .OrderBy(s => s.Naziv)
                .Select(s => new GradGetAll()
                {
                    id = s.Id,
                    postanskiBroj = s.PostanskiBroj,
                    naziv = s.Naziv,
                })
                .Take(100);
            return Ok(data.ToList());
        }
    }


}
