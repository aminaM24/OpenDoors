using System.ComponentModel.DataAnnotations.Schema;
namespace OpenDoors.Models
{
    [Table("KrajnjiKorisnik")]
    public class KrajnjiKorisnik : Korisnik
    {
        public string? Titula { get; set; }
        public bool? Registrovan { get; set; }
        public int? BrojRezervacija { get; set; }
        public int? BrojOtkazanihRezervacija { get; set; }
    }
}
