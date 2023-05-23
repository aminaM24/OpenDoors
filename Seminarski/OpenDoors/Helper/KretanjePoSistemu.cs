using OpenDoors.Data;
using OpenDoors.Helper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using OpenDoors.Models;

namespace OpenDoors.Helper
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage = null)
        {
            KorisnickiNalog korisnik = httpContext.GetLoginInformacije().korisnickiNalog;
            var request = httpContext.Request;
            var queryString = request.Query;
            if (queryString.Count == 0 && !request.HasFormContentType)
                return 0;

            string detalji = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }
            var x = new LogKretanjePoSistemu
            {
                korisnik = korisnik,
                vrijeme = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = detalji,
                ipAdresa = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };
            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            ApplicationDbContext? db = httpContext.RequestServices.GetService<ApplicationDbContext>();
            db.Add(x);
            db.SaveChanges();

            return x.id;
        }
    }
}