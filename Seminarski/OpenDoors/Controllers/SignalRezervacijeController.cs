using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OpenDoors.Data;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRezervacijeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<RezervacijeHub> _rezervacijeHub;

        public SignalRezervacijeController(ApplicationDbContext dbContext, IHubContext<RezervacijeHub> rezervacijeHub)
        {
            this._dbContext = dbContext;
            _rezervacijeHub = rezervacijeHub;
        }

        [HttpGet]
        public async Task<ActionResult> CountRezervacija(int nekId)
        {
            var _brojRezervacija = _dbContext.Rezervacija.Where(x => x.NekretninaId == nekId).ToList();
            await _rezervacijeHub.Clients.All.SendAsync("CountRezervacija", _brojRezervacija.Count());
            return Ok();
        }

    }
}
