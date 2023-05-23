using OpenDoors.Models;

namespace OpenDoors.ViewModels
{
    public class NekretninaGetAll
    {
        public int Id { get; set; }
        public int BrojKvadrata { get; set; }
        public int BrojSoba { get; set; }
        public int BrojKupatila { get; set; }
        public int BrojKreveta { get; set; }
        public double CijenaPoDanu { get; set; }
        public string Adresa { get; set; }
        public bool Avans { get; set; }
        public int? LokacijaId { get; set; }
        public string Lokacija { get; set; }
        public int TipId { get; set; }
        public string Tip { get; set; }
        public int VlasnikId { get; set; }
        public string ImeVlasnik { get; set; }
        public List<PogodnostiNekretnineGetAll> selectedPogodnosti { get;  set; }
        public List<int> slike_ids { get; set; }
    }
}
