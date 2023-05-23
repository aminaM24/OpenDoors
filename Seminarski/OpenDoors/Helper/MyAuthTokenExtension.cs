using OpenDoors.Data;
using OpenDoors.Helper;
using OpenDoors.Models;
using Microsoft.EntityFrameworkCore;
using OpenDoors.Models;
using System.Text.Json.Serialization;

namespace OpenDoors.Helper
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }
            [JsonIgnore]
            public KorisnickiNalog? korisnickiNalog => autentifikacijaToken?.korisnickiNalog;
            public AutentifikacijaToken autentifikacijaToken { get; set; }
            public bool isLogiran => korisnickiNalog != null;
        }

        public static LoginInformacije GetLoginInformacije(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();
            AutentifikacijaToken? korisnickiNalog = db.AutentifikacijaToken.Include(s => s.korisnickiNalog).SingleOrDefault(x => token != null && x.vrijednost == token);
            return korisnickiNalog;
        }

        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }


}