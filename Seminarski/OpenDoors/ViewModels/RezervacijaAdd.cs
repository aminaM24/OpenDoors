using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class RezervacijaAdd
    {
        public int BrojOsoba { get; set; }
        public int Djeca { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NekretninaId { get; set; }
        public int KorisnikId { get; set; }
        public int KreditnaKarticaId { get; set; }
    }
}
