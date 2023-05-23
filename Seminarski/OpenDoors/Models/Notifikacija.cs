using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Notifikacija
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }
        public bool Status { get; set; }
        [ForeignKey("KorisnickiNalogId")]
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int KorisnickiNalogId { get; set; }
        [ForeignKey("RezervacijaId")]
        public Rezervacija Rezervacija { get; set; }
        public int RezervacijaId { get; set; }
    }
}


