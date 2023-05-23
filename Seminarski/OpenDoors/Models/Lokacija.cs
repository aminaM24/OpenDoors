using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class Lokacija
    {
        [Key]
        public int Id { get; set; }
        public string DioGrada { get; set; }
        public string Naziv { get; set; }
        [ForeignKey("GradId")]
        public Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}

