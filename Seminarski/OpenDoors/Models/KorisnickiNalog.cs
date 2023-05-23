using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenDoors.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Verifikovan { get; set; }
        [JsonIgnore]
        public Vlasnik vlasnik => this as Vlasnik;

        [JsonIgnore]
        public KrajnjiKorisnik krajnjiKorisnik => this as KrajnjiKorisnik;
        public bool isVlasnik => vlasnik != null;
        public bool isKrajnjiKorisnik => krajnjiKorisnik != null;
    }
}

