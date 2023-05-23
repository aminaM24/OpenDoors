using OpenDoors.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class PosebnaPonuda
    {
        [Key]
        public int Id { get; set; }
        public string Opis { get; set; }
        public string Popust { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumIsteka { get; set; }
        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }
        public int NekretninaId { get; set; }
    }
}
