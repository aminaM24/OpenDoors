using OpenDoors.Models;

namespace OpenDoors.ViewModels
{
    public class RecenzijaGetAll
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public int NekretninaId { get; set; }
        public KorisnickiNalog Korisnik { get; set; }
        public string DatumPostavljanja { get; set; }
        public DateTime? DatumModifikacije { get; set; }
    }
}
