using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    [PrimaryKey(nameof(KrajnjiKorisnikId), nameof(PromoKodoviId))]
    public class KrajnjiKorisnikPromoKodovi
    {
       
        [ForeignKey("KrajnjiKorisnikId")]
        public KrajnjiKorisnik KrajnjiKorisnik { get; set; }
        public int KrajnjiKorisnikId { get; set; }
        [ForeignKey("PromoKodoviId")]
        public PromoKodovi PromoKodovi { get; set; }
        public int PromoKodoviId { get; set; }
    }
}
