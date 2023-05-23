using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenDoors.ViewModels
{
    public class SlikeAdd
    {
        public int Id { get; set; }
        public DateTime DatumPostavljanja { get; set; }
        public byte[] Slika { get; set; }
        public int NekretninaId { get; set; }
    }
}
