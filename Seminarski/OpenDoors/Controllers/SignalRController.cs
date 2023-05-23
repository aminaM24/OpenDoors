using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OpenDoors.Data;
using OpenDoors.Helper;

namespace OpenDoors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHubContext<RecenzijeHub> _recenzijeHub;

        public SignalRController(ApplicationDbContext dbContext, IHubContext<RecenzijeHub> recenzijeHub)
        {
            this._dbContext = dbContext;
            _recenzijeHub = recenzijeHub;
        }

        [HttpGet]
        public async Task<ActionResult> CountKomentara(int nekId)
        {
            var _brojKomentara = _dbContext.Recenzije.Where(x => x.NekretninaId == nekId).ToList();
            await _recenzijeHub.Clients.All.SendAsync("CountKomentara",_brojKomentara.Count());
            return Ok();
        }

    }
}
