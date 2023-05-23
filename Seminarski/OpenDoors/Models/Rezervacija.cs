using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public int Djeca { get; set; }
        public bool? Status { get; set; } = true;
        public DateTime? DatumOtkazivanja { get; set; }
        public double Cijena { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool PovratNovca { get; set; }
        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }
        public int NekretninaId { get; set; }
        [ForeignKey("KorisnikId")]
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        [ForeignKey("KreditnaKarticaId")]
        public KreditnaKartica KreditnaKartica { get; set; }
        public int KreditnaKarticaId { get; set; }
    }
}
