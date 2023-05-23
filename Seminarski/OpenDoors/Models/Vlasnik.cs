using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    [Table("Vlasnik")]
    public class Vlasnik : Korisnik
    {
        public int? BrojNekretnina { get; set; }
        public  DateTime? IznajmljivateljOd { get; set; }
        public string? Titula { get; set; }
    }
}
