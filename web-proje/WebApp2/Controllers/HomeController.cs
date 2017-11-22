using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp2.veri;

namespace WebApp2.Controllers
{
    public class HomeController : Controller
    {
        #region veritabaniBaglantisi
        // GET: Admin/Admin
        VeriContext db = new VeriContext();//Veri Tabani Baglantisi
        #endregion
        public ActionResult AnaSayfa()
        {
            return View();
        }
        public ActionResult Etkinlikler()
        {
            return View(db.Etkinlikler.ToList());
        }
        public ActionResult DersNotu()
        {
            return View();
        }
        public ActionResult Esya()
        {
            return View();
        }
        public ActionResult Tanitim()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BizeUlasın()
        {
            return View();
        }


    }
}