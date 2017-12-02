using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp2.Models;

namespace WebApp2.App_Classes
{
    public class GirisKontrol : FilterAttribute,IActionFilter
    {
        private string rollerim;
        public GirisKontrol(string rollerim)
        {
            this.rollerim = rollerim;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(HttpContext.Current.Session["Kullanici"]==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "AnaSayfa" } });
            }
            else
            {
                veri.VeriContext db = new veri.VeriContext();
                int KullaniciID = Convert.ToInt32(HttpContext.Current.Session["Kullanici"]);
                Kullanici kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == KullaniciID);
                string[] Roller = rollerim.Split(',');
                bool dogruMu = Roller.Contains(kullanici.Rol.RolAdi);
                if (!dogruMu)
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "Controller", "User" }, { "Action", "AnaSayfa" } });
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
    }
}


