using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Transakcija
    {
        [Key]
        public int Id { get; set; }
        public string Iznos { get; set; }
        public string Popust { get; set; }
        public string NacinPlacanja { get; set; }
        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }
        public int NekretninaId { get; set; }
        [ForeignKey("KorisnikId")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        [ForeignKey("KreditnaKarticaId")]
        public KreditnaKartica KreditnaKartica { get; set; }
        public int KreditnaKarticaId { get; set; }
        [ForeignKey("RezervacijaId")]
        public Rezervacija Rezervacija { get; set; }
        public int RezervacijaId { get; set; }


    }
}
