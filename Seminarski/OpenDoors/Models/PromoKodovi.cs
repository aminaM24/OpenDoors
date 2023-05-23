using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{ 
    public class PromoKodovi
    {
        [Key]
        public int Id { get; set; }
        public string Opis { get; set; }
        public string Popust { get; set; }
        public string Naziv { get; set; }
        public string Kod { get; set; }
        public bool Validan { get; set; }
    }
}
