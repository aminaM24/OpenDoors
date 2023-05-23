using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class KreditnaKarticaAdd
    {
        public string BrojKartice { get; set; }
        public string TipKartice { get; set; }
        public int CVV { get; set; }
        public DateTime datumIsteka { get; set; }
        public int KorisnikId { get; set; }
    }
}
