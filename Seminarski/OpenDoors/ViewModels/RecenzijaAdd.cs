using OpenDoors.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDoors.ViewModels
{
    public class RecenzijaAdd
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Komentar { get; set; }
        public int NekretninaId { get; set; }
        public int KorisnickiNalogId { get; set; }
    }
}
