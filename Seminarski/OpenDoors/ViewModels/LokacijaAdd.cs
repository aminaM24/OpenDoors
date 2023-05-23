using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class LokacijaAdd
    {
        public string DioGrada { get; set; }
        public string Naziv { get; set; }
        public int GradId { get; set; }
    }
}
