using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenDoors.Helper;

namespace OpenDoors.Helper
{
    public class AutorizacijaAtributte : TypeFilterAttribute
    {
        public AutorizacijaAtributte(bool krajnjiKorisnik, bool vlasnik) : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }

        public class MyAuthorizeImpl : IActionFilter
        {
            private readonly bool _KrajnjiKorisnik;
            private readonly bool _Vlasnik;

            public MyAuthorizeImpl(bool krajnjiKorisnik, bool vlasnik)
            {
                this._KrajnjiKorisnik = krajnjiKorisnik;
                this._Vlasnik = vlasnik;
            }
            public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
            {

            }
            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (filterContext.HttpContext.GetLoginInformacije().isLogiran)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
                KretanjePoSistemu.Save(filterContext.HttpContext);
                if (filterContext.HttpContext.GetLoginInformacije().isLogiran)
                {
                    return;
                }
                filterContext.Result = new UnauthorizedResult();
            }
        }

    }
}
