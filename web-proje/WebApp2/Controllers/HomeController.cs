using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

                
           return View();
        }
        public ActionResult Etkinlikler(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var EtkinliklerListesi = db.Etkinlikler.OrderByDescending(m => m.EtkinlikID).ToPagedList<Etkinlik>(_sayfaNo, 6);

            return View(EtkinliklerListesi);
            
        }
        public ActionResult EtkinlikDetay(int id)
        {
            Etkinlik etkinlik = db.Etkinlikler.FirstOrDefault(x => x.EtkinlikID == id);
            return View(etkinlik);
        }
        public ActionResult DersNotuDetay(int id)
        {
            DersNotu ders = db.Dersler.FirstOrDefault(x => x.DersNotuID == id);
            return View(ders);
        }
        public ActionResult DersNotu(int ? SayfaNo)
        {

            
            int _sayfaNo = SayfaNo ?? 1;
            var DersNotuListesi = db.Dersler.OrderByDescending(m => m.DersNotuID).ToPagedList<DersNotu>(_sayfaNo, 3);

            return View(DersNotuListesi);
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