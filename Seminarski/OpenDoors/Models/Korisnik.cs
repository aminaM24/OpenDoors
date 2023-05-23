using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    [Table("Korisnik")]
    
    public class Korisnik : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public int GodinaRodjenja { get; set; }
        public string BrojTelefona { get; set; }
        public byte[]? slikaKorisnika { get; set; }
        public string? Token { get; set; }
    }
}

