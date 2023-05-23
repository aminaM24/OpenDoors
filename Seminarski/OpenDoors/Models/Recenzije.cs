using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Recenzije
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public DateTime? DatumModifikacije { get; set; }
        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }
        public int NekretninaId { get; set; }
        [ForeignKey("KorisnickiNalogId")]
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int KorisnickiNalogId { get; set; }


    }
}
