using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class TipNekretnine
    {
        [Key]
        public int Id { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
    }
}
