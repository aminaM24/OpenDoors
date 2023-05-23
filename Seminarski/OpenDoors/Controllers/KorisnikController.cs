using GoDonate.Helper;
using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using OpenDoors.Helper;
using OpenDoors.ViewModels;

namespace OpenDoors.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class KorisnikController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public ActionResult PromijeniLozinku([FromBody] PromjenaLozinkeVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else if (korisnik.Password != x.staraLozinka)
                return BadRequest();
            else
            {
                korisnik.Password = x.novaLozinka;
                _dbContext.SaveChanges();
            }

            return Ok();
        }

        [HttpGet("{korisnikid}")]
        public ActionResult GetSlikuKorisnika(int korisnikid)
        {
            byte[]? sKorisnika = _dbContext.Korisnik.Find(korisnikid).slikaKorisnika;

            if (sKorisnika != null)
                return File(sKorisnika, "image/*");
            else
                return BadRequest("Korisnik nema profilnu sliku!");
        }

        [HttpPost]
        public ActionResult PromijeniSliku([FromBody] PromjenaSlikeVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.id);
            if (korisnik == null)
                return BadRequest();
            else
            {
                korisnik.slikaKorisnika = x.slikaKorisnika.ParsirajBase64();
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpPost]
        public bool Verifikuj([FromBody] TokenVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.korisnikId);
            if (x.token == korisnik.Token)
            {
                korisnik.Verifikovan = true;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;

        }

        [HttpPost]
        public ActionResult NovaLozinka([FromBody] NovaLozinkaVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.id);
            korisnik.Password = x.novaLozinka;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public ActionResult posaljiVerifikacijskiKod([FromBody] VerifikacijskiKodVM x)
        {
            var token = TokenGenerator.Generate(5);
            var poruka = $"Vaš novi verifikacijski kod je {token}.";
            var provjera = _dbContext.Korisnik.FirstOrDefault(s => s.Email == x.Email);
            EmailHelper.PosaljiEmail(provjera.Email, "Password recovery", poruka);
            provjera.Token = token;
            _dbContext.SaveChanges();
            return Ok(provjera.Id);
        }

        [HttpPost]
        public bool ProvjeriValidnost([FromBody] TokenVM x)
        {
            var korisnik = _dbContext.Korisnik.Find(x.korisnikId);
            if (korisnik.Token == x.token)
                return true;
            else
                return false;
        }

    }
}
