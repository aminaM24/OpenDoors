using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OpenDoors.Models
{
    public class Nekretnina
    {
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public int BrojKvadrata { get; set; }
        public int BrojSoba { get; set; }
        public int BrojKupatila { get; set; }
        public int BrojKreveta { get; set; }
        public double CijenaPoDanu { get; set; }
        public string Adresa { get; set; }
        public bool Avans { get; set; }
        [ForeignKey("LokacijaId")]
        public Lokacija Lokacija { get; set; }
        public int LokacijaId { get; set; }
        [ForeignKey("TipId")]
        public TipNekretnine Tip { get; set; }
        public int TipId { get; set; }
        [ForeignKey("VlasnikId")]
        public Vlasnik Vlasnik { get; set; }
        public int VlasnikId { get; set; }

    }
}
