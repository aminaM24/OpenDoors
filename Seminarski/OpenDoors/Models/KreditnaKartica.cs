using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class KreditnaKartica
    {
        [Key]
        public int Id { get; set; }
        public string BrojKartice { get; set; }
        public string TipKartice { get; set; }
        public DateTime datumIsteka { get; set; }
        public int CVV { get; set; }
        public string? Status { get; set; }
        [ForeignKey("KorisnikId")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
    }
}

