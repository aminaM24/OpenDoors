using Microsoft.EntityFrameworkCore;
using OpenDoors.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.Models
{
    public class NekretninaPogodnostiNekretnineAdd
    {
        public int NekretninaId { get; set; }
        public int PogodnostiNekretnineId { get; set; }
    }
}

