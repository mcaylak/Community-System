using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp2.App_Classes
{
    public class GirisKontrol : FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(HttpContext.Current.Session["Kullanici"]==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "AnaSayfa" } });
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }
    }
}