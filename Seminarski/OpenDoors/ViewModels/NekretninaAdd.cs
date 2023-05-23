using OpenDoors.Models;

namespace OpenDoors.ViewModels
{
    public class NekretninaAdd
    {
        public int Id { get; set; }
        public int BrojKvadrata { get; set; }
        public int BrojSoba { get; set; }
        public int BrojKupatila { get; set; }
        public int BrojKreveta { get; set; }
        public double CijenaPoDanu { get; set; }
        public string Adresa { get; set; }
        public bool Avans { get; set; }
        public int LokacijaId { get; set; }
        public int TipId { get; set; }
        public string Tip { get; set; }
        public int VlasnikId { get; set; } 
        public List<string> slike{ get; set; } 
        public List<int> selectedPogodnosti { get; set; }       
    }
}
