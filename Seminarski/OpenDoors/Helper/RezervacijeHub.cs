using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Data;

namespace OpenDoors.Helper
{
    public class RezervacijeHub:Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<RezervacijeHub> _rezervacijeHub;
        public RezervacijeHub(ApplicationDbContext dbContext, IHubContext<RezervacijeHub> rezervacijeHub)
        {
            this._dbContext = dbContext;
            _rezervacijeHub = rezervacijeHub;
        }
        public async Task ProslijediPoruku(int nekId)
        {
            var _brojRezervacija = _dbContext.Rezervacija.Where(x => x.NekretninaId == nekId).ToList();
            await _rezervacijeHub.Clients.All.SendAsync("CountRezervacija", _brojRezervacija.Count());
        }
    }
}
