using Microsoft.EntityFrameworkCore;
using OpenDoors.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    [PrimaryKey(nameof(NekretninaId), nameof(PogodnostiNekretnineId))]
    public class NekretninaPogodnostiNekretnine
    {
        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }
        public int NekretninaId { get; set; }
        [ForeignKey("PogodnostiNekretnineId")]
        public PogodnostiNekretnine PogodnostiNekretnine { get; set; }
        public int PogodnostiNekretnineId { get; set; }
    }
}
