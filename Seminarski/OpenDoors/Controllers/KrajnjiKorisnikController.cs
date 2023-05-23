using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class KrajnjiKorisnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KrajnjiKorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public KrajnjiKorisnik Snimi([FromBody] KrajnjiKorisnikAdd x)
        {

            var korisnik = new KrajnjiKorisnik
            {
               Ime=x.Ime,
               Prezime=x.Prezime,
               Spol=x.Spol,
               GodinaRodjenja=x.GodinaRodjenja,
               BrojTelefona=x.BrojTelefona,
               Id=x.Id,
               Username=x.Username,
               Password=x.Password,
               Email=x.Email,
               slikaKorisnika = x.slikaKorisnika?.ParsirajBase64()
            };

            _dbContext.Add(korisnik);
            _dbContext.SaveChanges();
            return korisnik;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.KrajnjiKorisnik
                .OrderBy(s => s.Ime)
                .Select(s => new KrajnjiKorisnikGetAll()
                {
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Spol = s.Spol,
                    GodinaRodjenja = s.GodinaRodjenja,
                    BrojTelefona = s.BrojTelefona,
                    Id = s.Id,
                    Username = s.Username,
                    Password = s.Password,
                    Email = s.Email
                })
                .Take(100);
            return Ok(data.ToList());
        }
      
    }


}

