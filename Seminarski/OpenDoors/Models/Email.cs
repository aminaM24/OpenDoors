using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public string Sadrzaj { get; set; }
        public string Predmet { get; set; }
        [ForeignKey("KorisnickiNalogId")]
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int KorisnickiNalogId { get; set; }
    }
}

