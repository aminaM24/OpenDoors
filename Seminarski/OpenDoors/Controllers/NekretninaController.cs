using OpenDoors.Data;
using OpenDoors.Models;
using OpenDoors.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NekretninaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public NekretninaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost("{ID}")]
        public ActionResult Delete(int id)
        {
            
            var nekretnina = _dbContext.Nekretnina.Find(id);

            if (nekretnina == null)
                return BadRequest("pogresan ID");


            if (nekretnina != null)
            {

                 var rezNekretnine = _dbContext.Rezervacija.Where(s => s.NekretninaId == id).ToList();
                    if (rezNekretnine != null)
                        foreach (var rez in rezNekretnine)
                            _dbContext.Remove(rez);
                var slikeNekretnine = _dbContext.Slike.Where(s => s.NekretninaId == id).ToList();
                if (slikeNekretnine != null)
                    foreach (var s in slikeNekretnine)
                        _dbContext.Remove(s);
                var pogodnostiNekretnine = _dbContext.NekretninaPogodnostiNekretnine.Where(y => y.NekretninaId == id).ToList();
                if (pogodnostiNekretnine != null)
                    foreach (var p in pogodnostiNekretnine)
                        _dbContext.Remove(p);
                var recenzijeNekretnine = _dbContext.Recenzije.Where(y => y.NekretninaId == id).ToList();
                if (recenzijeNekretnine != null)
                    foreach (var r in recenzijeNekretnine)
                        _dbContext.Remove(r);
            }
          
            _dbContext.Remove(nekretnina);

            _dbContext.SaveChanges();
            return Ok(nekretnina);
        }


        [HttpPost]
        public Nekretnina Snimi([FromBody] NekretninaAdd x)
        {
            Nekretnina? objekat;

            if (x.Id == 0)
            {
                objekat = new Nekretnina();
                _dbContext.Add(objekat);//priprema sql
            }
            else
            {
                objekat = _dbContext.Nekretnina.Find(x.Id);
            }

            objekat.BrojSoba = x.BrojSoba;
            objekat.BrojKupatila = x.BrojKupatila;
            objekat.BrojKvadrata = x.BrojKvadrata;
            objekat.Adresa = x.Adresa;
            objekat.Avans = x.Avans;
            objekat.BrojKreveta = x.BrojKreveta;
            objekat.CijenaPoDanu = x.CijenaPoDanu;
            objekat.LokacijaId = x.LokacijaId;
            objekat.TipId = x.TipId;
            objekat.VlasnikId = x.VlasnikId;


            _dbContext.SaveChanges();

            if (x.Id == 0)
            {
                foreach (var s in x.slike)
                {
                    var noviSlika = new Slike
                    {
                        Slika = s?.ParsirajBase64(),
                        Nekretnina = objekat,
                        DatumPostavljanja = DateTime.Now
                    };
                    _dbContext.Add(noviSlika);
                    _dbContext.SaveChanges(); ;
                }

                foreach (var s in x.selectedPogodnosti)
                {
                    var n = new NekretninaPogodnostiNekretnine
                    {
                        Nekretnina = objekat,
                        PogodnostiNekretnineId = s
                    };
                    _dbContext.Add(n);
                    _dbContext.SaveChanges();
                }
            }            

            if (x.Id != 0)
            {
                foreach (var s in x.selectedPogodnosti)
                {
                    var postojecePog = _dbContext.NekretninaPogodnostiNekretnine.Where(y => y.NekretninaId == x.Id).Select(y => y.PogodnostiNekretnineId).ToList();
                    if (!postojecePog.Contains(s))
                    {
                        var n = new NekretninaPogodnostiNekretnine
                        {
                            Nekretnina = objekat,
                            PogodnostiNekretnineId = s
                        };
                        _dbContext.Add(n);
                        _dbContext.SaveChanges();
                    }
                }

                var postojecePogodnosti = _dbContext.NekretninaPogodnostiNekretnine.Where(y => y.NekretninaId == x.Id).Select(y => y.PogodnostiNekretnineId).ToList();

                foreach (var p in postojecePogodnosti)
                if (!x.selectedPogodnosti.Contains(p))
                {
                    NekretninaPogodnostiNekretnine? nekretninaPogodnost = _dbContext.NekretninaPogodnostiNekretnine.Where(y => y.NekretninaId == x.Id && y.PogodnostiNekretnineId == p).FirstOrDefault();

                    if (nekretninaPogodnost != null)
                    {
                        _dbContext.Remove(nekretninaPogodnost);
                        _dbContext.SaveChanges();
                    }
                }
            }

            _dbContext.SaveChanges();
            return objekat;
        }
        
        [HttpGet]
        public ActionResult GetAll()
        {
            var data = _dbContext.Nekretnina
                .OrderBy(s => s.Adresa)
                .Select(s => new
                {
                    id = s.Id,
                    brojKvadrata = s.BrojKvadrata,
                    brojKupatila = s.BrojKupatila,
                    brojSoba = s.BrojSoba,
                    brojKreveta = s.BrojKreveta,
                    avans = s.Avans,
                    adresa = s.Adresa,
                    cijenaPoDanu = s.CijenaPoDanu,
                    lokacijaId = s.LokacijaId,
                    lokacija = s.Lokacija.DioGrada,
                    tip = s.Tip,
                    tipId = s.TipId,
                    vlasnikId = s.VlasnikId,
                    vlasnik = s.Vlasnik.Ime + " " + s.Vlasnik.Prezime,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),
                                      
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpGet]
        public List<NekretninaGetAll> GetByTip(int tipId)
        {
            var data = _dbContext.Nekretnina.Where(x => x.TipId == tipId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKreveta,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija = s.Lokacija.DioGrada,
                    Tip = s.Tip.Opis,
                    VlasnikId = s.VlasnikId,
                    TipId = s.TipId,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<NekretninaGetAll> GetById(int nekretninaId)
        {
            var pogodnosti = _dbContext.NekretninaPogodnostiNekretnine.Where(x => x.NekretninaId == nekretninaId);
            var data = _dbContext.Nekretnina.Where(x => x.Id == nekretninaId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKreveta,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija = s.Lokacija.DioGrada,
                    VlasnikId = s.VlasnikId,
                    ImeVlasnik = s.Vlasnik.Ime + " " + s.Vlasnik.Prezime,
                    TipId = s.TipId,
                    Tip = s.Tip.Opis,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),
                    selectedPogodnosti = pogodnosti.Select(x=> new PogodnostiNekretnineGetAll
                    {
                        Id=x.PogodnostiNekretnineId,
                        Naziv=x.PogodnostiNekretnine.Naziv
                    }).ToList()
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<NekretninaGetAll> GetByVlasnik(int korisnickiNalogId)
        {
            
            var data = _dbContext.Nekretnina.Where(x => x.VlasnikId ==korisnickiNalogId )
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKreveta,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija = s.Lokacija.DioGrada,
                    VlasnikId = s.VlasnikId,
                    ImeVlasnik = s.Vlasnik.Ime + " " + s.Vlasnik.Prezime,
                    TipId = s.TipId,
                    Tip = s.Tip.Opis,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),

             })
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpGet]
        public List<NekretninaGetAll> GetAllOsimVlasnika(int korisnickiNalogId)
        {

            var data = _dbContext.Nekretnina.Where(x => x.VlasnikId != korisnickiNalogId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKreveta,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija = s.Lokacija.DioGrada,
                    VlasnikId = s.VlasnikId,
                    ImeVlasnik = s.Vlasnik.Ime + " " + s.Vlasnik.Prezime,
                    TipId = s.TipId,
                    Tip = s.Tip.Opis,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),

                })
                .AsQueryable();
            return data.Take(100).ToList();
        }


        [HttpGet]
        public List<NekretninaGetAll> GetByTipOsimVlasnika(int tipId, int korisnickiNalogId)
        {
            var data = _dbContext.Nekretnina.Where(x => x.TipId == tipId && x.VlasnikId != korisnickiNalogId)
                .OrderBy(s => s.TipId)
                .Select(s => new NekretninaGetAll
                {
                    Id = s.Id,
                    BrojKvadrata = s.BrojKvadrata,
                    BrojKupatila = s.BrojKupatila,
                    BrojSoba = s.BrojSoba,
                    BrojKreveta = s.BrojKreveta,
                    Adresa = s.Adresa,
                    CijenaPoDanu = s.CijenaPoDanu,
                    Avans = s.Avans,
                    LokacijaId = s.LokacijaId,
                    Lokacija = s.Lokacija.DioGrada,
                    Tip = s.Tip.Opis,
                    VlasnikId = s.VlasnikId,
                    TipId = s.TipId,
                    slike_ids = _dbContext.Slike.Where(w => w.NekretninaId == s.Id).Select(w => w.Id).ToList(),
                })
                .AsQueryable();
            return data.Take(100).ToList();
        }
    }
}

