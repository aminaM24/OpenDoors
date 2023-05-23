using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OpenDoors.Data;
using Microsoft.VisualBasic;
using System;

namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class RezervacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RezervacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpPost]
        public ActionResult<Rezervacija> Snimi([FromBody] RezervacijaAdd x)
        {
            var cijenaPoDanu = _dbContext.Nekretnina.Find(x.NekretninaId).CijenaPoDanu;
            var rezervacijeNekretnine = _dbContext.Rezervacija.Where(y => y.NekretninaId == x.NekretninaId).ToList();
            foreach(var rez in rezervacijeNekretnine)
            {
                if (x.CheckIn.IsBetween(rez.CheckIn, rez.CheckOut) || x.CheckOut.IsBetween(rez.CheckIn, rez.CheckOut))
                    return BadRequest("Nekretnina je zauzeta u izabranom periodu.");
            }            

            var rezervacija = new Rezervacija
            {
               BrojOsoba=x.BrojOsoba,
               Djeca=x.Djeca,
               Status=true,
               CheckIn=x.CheckIn,
               CheckOut=x.CheckOut,
               Cijena= (x.CheckOut.Subtract(x.CheckIn)).Days==0?cijenaPoDanu: (x.CheckOut.Subtract(x.CheckIn)).Days * cijenaPoDanu ,
               NekretninaId=x.NekretninaId,
               KorisnikId=x.KorisnikId,
               KreditnaKarticaId=x.KreditnaKarticaId
            };

            _dbContext.Add(rezervacija);
            _dbContext.SaveChanges();
            return rezervacija;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Rezervacija
                .OrderBy(s => s.NekretninaId)
                .Select(s => new
                {
                    BrojOsoba = s.BrojOsoba,
                    Djeca = s.Djeca,
                    Status = s.Status,
                    CheckIn = s.CheckIn,
                    CheckOut = s.CheckOut,
                    Cijena = s.Cijena,
                    NekretninaId = s.NekretninaId,
                    KorisnikId = s.KorisnikId,
                    KreditnaKartica=s.KreditnaKartica.TipKartice + s.KreditnaKartica.BrojKartice,
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }
        [HttpGet]
        public List<RezervacijaGetAll> GetById(int nekretninaId)
        {
            
            var data = _dbContext.Rezervacija.Where(x => x.NekretninaId == nekretninaId)
                .OrderBy(s => s.Id)
                .Select(s => new RezervacijaGetAll
                {
                    Id=s.Id,
                    BrojOsoba = s.BrojOsoba,
                    Djeca = s.Djeca,
                    Status = s.Status,
                    CheckIn = s.CheckIn.ToString("MM/dd/yyyy"),
                    CheckOut = s.CheckOut.ToString("MM/dd/yyyy"),
                    Cijena = s.Cijena,
                    Nekretnina = s.Nekretnina.Adresa+ ", " + s.Nekretnina.Lokacija.Naziv,
                    Korisnik = s.Korisnik.Ime+ " "+s.Korisnik.Prezime,
                    KreditnaKartica = s.KreditnaKartica.TipKartice + s.KreditnaKartica.BrojKartice,
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
        [HttpGet]
        public List<RezervacijaGetAll> GetByKorisnikId(int korisnikId)
        {

            var data = _dbContext.Rezervacija.Where(x => x.KorisnikId == korisnikId)
                .OrderBy(s => s.Id)
                .Select(s => new RezervacijaGetAll
                {
                    Id = s.Id,
                    BrojOsoba = s.BrojOsoba,
                    Djeca = s.Djeca,
                    Status = s.Status,
                    CheckIn = s.CheckIn.ToString("MM/dd/yyyy"),
                    CheckOut = s.CheckOut.ToString("MM/dd/yyyy"),
                    Cijena = s.Cijena,
                    Nekretnina = s.Nekretnina.Adresa + ", " + s.Nekretnina.Lokacija.Naziv,
                    Korisnik = s.Korisnik.Ime + " " + s.Korisnik.Prezime,
                    KreditnaKartica = s.KreditnaKartica.TipKartice + s.KreditnaKartica.BrojKartice,
                    NekretninaId = s.NekretninaId
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPost("{ID}")]
        public ActionResult OtkaziRezervaciju(int id)
        {
            Rezervacija? rezervacija = _dbContext.Rezervacija.Find(id);

            if (rezervacija == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(rezervacija);

            _dbContext.SaveChanges();
            return Ok(rezervacija);
        }

        [HttpPost]
        public ActionResult promjeniStatus(int nekretninaId)
        {
            var rezervacije = _dbContext.Rezervacija.Where(x=>x.NekretninaId==nekretninaId).ToList();

            if(rezervacije!=null)
                foreach(var rez in rezervacije)
                    if (rez.CheckOut < DateTime.Now)
                        rez.Status = false;

            _dbContext.SaveChanges();
            return Ok(rezervacije);
        }
    }
}


