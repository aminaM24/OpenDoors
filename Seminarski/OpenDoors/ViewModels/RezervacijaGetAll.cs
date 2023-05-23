namespace OpenDoors.ViewModels
{
    public class RezervacijaGetAll
    {
        public int NekretninaId { get; set; }
        public int Id { get; set; }
        public int BrojOsoba { get; set; }
        public int Djeca { get; set; }
        public bool? Status { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public double Cijena { get; set; }
        public string Nekretnina { get; set; }
        public string Korisnik { get; set; }
        public string KreditnaKartica {get; set; }

    }
}
