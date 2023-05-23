namespace OpenDoors.ViewModels
{
    public class KreditnaKarticaGetAll
    {
        public int Id { get; set; }
        public string BrojKartice { get; set; }
        public string TipKartice { get; set; }
        public int KorisnikId { get; set; }
        public DateTime datumIsteka { get; set; }
        public int CVV { get; set; }
    }
}
