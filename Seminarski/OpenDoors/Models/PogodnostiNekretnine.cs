using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace OpenDoors.Models
{
    public class PogodnostiNekretnine
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string? Opis { get; set; }
    }
}
