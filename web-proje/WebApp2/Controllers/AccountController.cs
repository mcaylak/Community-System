using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp2.Models;
using WebApp2.veri;

namespace WebApp2.Controllers
{
    public class AccountController : Controller
    {
        #region veritabaniBaglantisi
        // GET: Admin/Admin
        VeriContext db = new VeriContext();//Veri Tabani Baglantisi
        #endregion
        // GET: Account
        public ActionResult UyeKayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeKayit(Kullanici kullanici)
        {
            Kullanici yenikayit = new Kullanici();
            yenikayit.KullaniciAdi = kullanici.KullaniciAdi;
            yenikayit.KullaniciSoyadi = kullanici.KullaniciSoyadi;
            yenikayit.KullaniciMail = kullanici.KullaniciMail;
            yenikayit.KullaniciSifre = kullanici.KullaniciSifre;
            yenikayit.KullaniciKayitTarihi = DateTime.Now;

            Rol rol = db.Roller.FirstOrDefault(x => x.RolAdi=="User");
            yenikayit.RolID = rol.RolId;
     

            db.Kullanicilar.Add(yenikayit);
            db.SaveChanges();
            return RedirectToAction("AnaSayfa", "Home");
        }
        public ActionResult UyeGiris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return Redirect("/Admin/Home");
        }
        [HttpPost]
        public ActionResult UyeGiris(string kullaniciMail,string kullaniciSifre)
        {
            var query = db.Kullanicilar.Where(x => x.KullaniciMail == kullaniciMail).FirstOrDefault();
            if (query!=null)
            {
                if (query.KullaniciSifre == kullaniciSifre && query.KullaniciMail == kullaniciMail)
                {
                    Session["Kullanici"] = query.KullaniciID;
                }
            }
                       
            return RedirectToAction("AnaSayfa", "Home");
        }
        public ActionResult LogOff()
        {
            Session["Kullanici"] = null;
            return RedirectToAction("AnaSayfa", "Home");
        }
        public ActionResult HesapAyarlari(int KullaniciID)
        {
            Kullanici kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == KullaniciID);
            int id = Convert.ToInt32(Session["Kullanici"]);
            ViewData["etkinlikTalep"] = db.Etkinlikler.Where(x => x.etkinlikSahibiId == id).Take(5).ToList();
            ViewData["dersTalep"] = db.Dersler.Where(x => x.DersNotuSahibiId == id).Take(5).ToList();
           
            return View(kullanici);
        }
        [HttpPost]
        public ActionResult HesapAyarlari(Kullanici Kullanici)
        {
            Kullanici kullaniciDuzenle = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == Kullanici.KullaniciID);
            kullaniciDuzenle.KullaniciAdi = Kullanici.KullaniciAdi;
            kullaniciDuzenle.KullaniciMail = Kullanici.KullaniciMail;
            kullaniciDuzenle.KullaniciSoyadi = Kullanici.KullaniciSoyadi;

            
            db.SaveChanges();

            return RedirectToAction("AnaSayfa", "Home");
        }
        [HttpPost]
        public ActionResult HesapParolaAyarlari(Kullanici Kullanici,string parola)
        {
            Kullanici kullaniciDuzenle = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == Kullanici.KullaniciID);
            if (kullaniciDuzenle.KullaniciSifre==parola)
            {
                kullaniciDuzenle.KullaniciSifre = Kullanici.KullaniciSifre;
                ViewBag.bilgi = "Parola Basariyla Degistirildi";
            }
            else
            {
                ViewBag.bilgi = "Parola Degiştirme İşleminde Hata";
            }
            db.SaveChanges();
            
            return RedirectToAction("AnaSayfa", "Home");
        }
        [HttpGet]
        public ActionResult EtkinlikOnay(int id)
        {
            int KullaniciId = Convert.ToInt32(Session["Kullanici"]);
            Kullanici kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == KullaniciId);
            Etkinlik etkinlik = db.Etkinlikler.FirstOrDefault(x=>x.EtkinlikID==id);

            etkinlik.Kullanicilar.Add(kullanici);
            db.SaveChanges();

            return RedirectToAction("Etkinlikler", "Home");
        }
        [HttpGet]
        public ActionResult EtkinlikIptal(int id)
        {
            int KullaniciId = Convert.ToInt32(Session["Kullanici"]);
            Kullanici kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == KullaniciId);
            Etkinlik etkinlik = db.Etkinlikler.FirstOrDefault(x => x.EtkinlikID == id);

            etkinlik.Kullanicilar.Remove(kullanici);
            db.SaveChanges();

            return RedirectToAction("Etkinlikler", "Home");
        }
       
    }
}