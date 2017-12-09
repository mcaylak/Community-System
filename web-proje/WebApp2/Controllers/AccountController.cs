using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var sorgu = db.Kullanicilar.FirstOrDefault(x => x.KullaniciMail == kullanici.KullaniciMail);
            var query = db.Kullanicilar.FirstOrDefault(x => x.KullaniciSifre == kullanici.KullaniciSifre && x.KullaniciMail == kullanici.KullaniciMail);
            if (query!=null)
            {
                return RedirectToAction("UyeKayit");
            }

            if (sorgu==null)
            {
                Kullanici yenikayit = new Kullanici();
                yenikayit.KullaniciAdi = kullanici.KullaniciAdi;
                yenikayit.KullaniciSoyadi = kullanici.KullaniciSoyadi;
                yenikayit.KullaniciMail = kullanici.KullaniciMail;
                yenikayit.KullaniciSifre = kullanici.KullaniciSifre;
                yenikayit.KullaniciKayitTarihi = DateTime.Now;

                Rol rol = db.Roller.FirstOrDefault(x => x.RolAdi == "User");
                yenikayit.RolID = rol.RolId;

                Guid Kontrol;
                Kontrol = Guid.NewGuid();
                //Confirm Email Gonderme
                #region mailGonderme
                var fromAddress = new MailAddress("m.recep.caylak@gmail.com", "SauHub");
                var toAddress = new MailAddress(yenikayit.KullaniciMail, "To Name");
                const string fromPassword = "<321<Aa<";
                string subject = "SauHub Hoşgeldiniz";
                string body = "Merhaba "+yenikayit.KullaniciAdi+
                    "...Aramıza katıldıgın için teşekkür ederiz. Sitemizde bulunan etkinlik sayfası ile ister üniversitemizde bulanan" +
                    " toplulukların yapmıs oldugu etkinliklere katılabilirsin ister kendi etkinlik fikrini paylaşma şansı yakalayabilirsin.Ders/Notu sayfası ile arkadaşlarımızın " +
                    "paylaşmıs oldugu ders notlarına kolaylıklar erişebilirsin istersen sende ders notlarını paylaşma şansı yakalayabilirsin.Yardım gerektigi zaman bizlere her saat" +
                    "Bize ulaşın formunu doldurarak ulaşabilirsiniz.."+"  ARTIK HESABINI AKTİF EDEREK BAŞLAYABİLİRSİN  "+"  AKTİVASYON KODU ..="+Kontrol.ToString();
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                #endregion

                yenikayit.KontrolKodu = Kontrol;
                yenikayit.KontrolKoduDurum = false;


                db.Kullanicilar.Add(yenikayit);
                db.SaveChanges();
                return RedirectToAction("AnaSayfa", "Home");
            }
            else
            {
                if (query!=null)
                {
                    ViewBag.MailKontrol = "Girdiğiniz mail sitemizde kayıtlıdır.Lütfen farklı bir mail adresi kullanınız ya da şifrenizi hatırlamıyorsanız şifremi unuttum ile şifrenizi yenileyebilirsiniz..";
                    return View();
                }
            }
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
        public ActionResult UyeGiris(string kullaniciMail,string kullaniciSifre,string KontrolKodu,int? id)
        {
            var query = db.Kullanicilar.Where(x => x.KullaniciMail == kullaniciMail).FirstOrDefault();
            
            var sorgu = db.Kullanicilar.Where(x => x.KullaniciID == id).FirstOrDefault();
            if (sorgu!=null)
            {
                if (KontrolKodu == sorgu.KontrolKodu.ToString())
                {
                    sorgu.KontrolKoduDurum = true;
                    db.SaveChanges();
                    ViewBag.Kontrol = "Hesabınız Aktif Edilmistir.Hesabınıza Giris Yapabilirsiniz.";
                    return View(sorgu);
                }
            }
            if (query == null)
            {
                return RedirectToAction("UyeKayit");
            }
            if (query.KontrolKoduDurum != false)
            {
                if (query != null)
                {
                    if (query.KullaniciSifre == kullaniciSifre && query.KullaniciMail == kullaniciMail)
                    {
                        Session["Kullanici"] = query.KullaniciID;
                    }
                }
                return RedirectToAction("AnaSayfa", "Home");
            }
            else
            {
                return View(query);
            }
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
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }
       [HttpPost]
        public ActionResult ParolamiUnuttum(string email)
        {
            var sorgu = db.Kullanicilar.FirstOrDefault(x => x.KullaniciMail == email);
            if (sorgu != null)
            {

                var fromAddress = new MailAddress("m.recep.caylak@gmail.com", "SauHub");
                var toAddress = new MailAddress(email, "To Name");
                const string fromPassword = "<321<Aa<";
                string subject = "SauHub Hoşgeldiniz";
                string body = "Merhaba şifre degiştirme talebi göndermediyseniz bu maili dikkate almayınız . Eger şifre degiştirmek istiyorsanız kullanacıgız kod...:" + sorgu.KontrolKodu;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }


                return View(sorgu);
            }
            else
                return RedirectToAction("AnaSayfa","Home");
        }
        [HttpGet]
        public ActionResult ParolamiUnuttumGuncelleme(string kontrolKodu, int id)
        {
            var kullanici = db.Kullanicilar.FirstOrDefault(x=>x.KullaniciID== id);

            if (kullanici!=null)
            {
                string kontrol = kullanici.KontrolKodu.ToString();
                if (kontrol == kontrolKodu)
                {
                    return View(kullanici);
                }
            }           
            return RedirectToAction("AnaSayfa","Home");
        }
        [HttpPost]
        public ActionResult ParolaKaydet(string parola,int id)
        {
            var kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciID == id);
            kullanici.KullaniciSifre = parola;
            ViewBag.ParolaDegistirildi = "Parolanız Başarıyla Değiştirildi.";
            db.SaveChanges();
            return RedirectToAction("AnaSayfa", "Home");
        }
    }
}