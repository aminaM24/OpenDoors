using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Data;

namespace OpenDoors.Helper
{
    public class RecenzijeHub:Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<RecenzijeHub> _recenzijeHub;
        public RecenzijeHub(ApplicationDbContext dbContext, IHubContext<RecenzijeHub> recenzijeHub)
        {
            this._dbContext = dbContext;
            _recenzijeHub = recenzijeHub;
        }
        public async Task ProslijediPoruku(int nekId)
        {
            var _brojKomentara = _dbContext.Recenzije.Where(x => x.NekretninaId == nekId).ToList();
            await _recenzijeHub.Clients.All.SendAsync("CountKomentara", _brojKomentara.Count());
        }
    }
}
