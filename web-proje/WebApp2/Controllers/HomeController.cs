using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApp2.Models;
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
        [HttpPost]
        public ActionResult BizeUlasinMail(string adi,string email,string telefonNo,string universite,string icerik)
        {
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.EnableSsl = true;
            WebMail.UserName = "m.recep.caylak@gmail.com";
            WebMail.Password = "<321<Aa<"; // gerçek dışı
            WebMail.SmtpPort = 587;
            WebMail.Send(
                    "m.recep.caylak@gmail.com",
                    adi,
                    telefonNo,
                    universite,
                    icerik
                );
            return View();
        }
        public ActionResult Etkinlikler()
        {
            return View(db.Etkinlikler.ToList());
        }
        public ActionResult EtkinlikDetay()
        {
            return View(db.Etkinlikler.ToList());
        }
        public ActionResult DersNotuDetay()
        {
            return View();
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