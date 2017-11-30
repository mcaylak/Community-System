using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            var EtkinliklerListesi = db.Etkinlikler.Where(m => m.EtkinlikDurum=="1").OrderByDescending(m => m.EtkinlikID).ToPagedList<Etkinlik>(_sayfaNo, 6);

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
            var DersNotuListesi = db.Dersler.Where(m => m.DersNotuDurum=="1").OrderByDescending(m => m.DersNotuID).ToPagedList<DersNotu>(_sayfaNo, 3);
            return View(DersNotuListesi);
        }
        public ActionResult DersNotuTalep()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DersNotuTalep(DersNotu ders, HttpPostedFileBase dersNotuResimYol)
        {
            DersNotu kullaniciNot = new DersNotu();
            kullaniciNot.DersNotuSahibiId = Convert.ToInt32(Session["Kullanici"]);
            int id = Convert.ToInt32(Session["Kullanici"]);
            var kullaniciAdi = db.Kullanicilar.FirstOrDefault(x=>x.KullaniciID==id).KullaniciAdi;
            kullaniciNot.paylasanAdi = kullaniciAdi;
            kullaniciNot.DersResimYol = DersNotuResimEkle(dersNotuResimYol);
            kullaniciNot.DersBuyukResimYol = DersNotuBuyukResimEkle(dersNotuResimYol);
            kullaniciNot.DersAdi = ders.DersAdi;
            kullaniciNot.DersBaslıgı = ders.DersBaslıgı;
            kullaniciNot.DersNotuAciklama = ders.DersNotuAciklama;
            kullaniciNot.DersNotuPaylasmaTarihi = DateTime.Now;
            kullaniciNot.DersNotuDurum = "0";
            db.Dersler.Add(kullaniciNot);
            db.SaveChanges();

            return RedirectToAction("DersNotu");
        }
        private string DersNotuResimEkle(HttpPostedFileBase DersNotuResimYol)
        {
            Image image = Image.FromStream(DersNotuResimYol.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 301, Height = 251 });
            string uzanti = System.IO.Path.GetExtension(DersNotuResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "/Content/DersNotuImgMedium/" + isim + uzanti;

            bimage.Save(Server.MapPath(yol));

            return yol;
        }
        private string DersNotuBuyukResimEkle(HttpPostedFileBase DersNotuResimYol)
        {
            Image image = Image.FromStream(DersNotuResimYol.InputStream);
            Bitmap bimageBig = new Bitmap(image, new Size { Width = 845, Height = 450 });
            string uzanti = System.IO.Path.GetExtension(DersNotuResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string buyukResimYol = "/Content/DersNotuImgBig/" + isim + uzanti;
            bimageBig.Save(Server.MapPath(buyukResimYol));
            return buyukResimYol;
        }
        public ActionResult EtkinlikEklemeTaleb()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EtkinlikEklemeTaleb(Etkinlik etkinlik, HttpPostedFileBase etkinlikResimYol)
        {
            Etkinlik etkinlikEkle = new Etkinlik();

            etkinlikEkle.etkinlikSahibiId = Convert.ToInt32(Session["Kullanici"]);
            etkinlikEkle.EtkinlikResimYol = EtkinlikResimEkle(etkinlikResimYol);
            etkinlikEkle.EtkinlikBuyukResimYol = EtkinlikBuyukResimEkle(etkinlikResimYol);
            etkinlikEkle.EtkinlikBasligi = etkinlik.EtkinlikBasligi;
            etkinlikEkle.EtkinlikAdres = etkinlik.EtkinlikAdres;
            etkinlikEkle.EtkinlikBitis = etkinlik.EtkinlikBitis;
            etkinlikEkle.EtkinlikTarihi = etkinlik.EtkinlikTarihi;
            etkinlikEkle.EtkinlikSahibi = etkinlik.EtkinlikSahibi;
            etkinlikEkle.EtkinlikIcerik = etkinlik.EtkinlikIcerik;
            etkinlikEkle.EtkinlikDurum = "0";

            db.Etkinlikler.Add(etkinlikEkle);
            db.SaveChanges();

            return RedirectToAction("Etkinlikler");
        }

        private string EtkinlikBuyukResimEkle(HttpPostedFileBase etkinlikResimYol)
        {
            Image image = Image.FromStream(etkinlikResimYol.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 1170, Height = 520 });

            string uzanti = System.IO.Path.GetExtension(etkinlikResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string BuyukResimYol = "/Content/EtkinlikImgBig/" + isim + uzanti;

            bimage.Save(Server.MapPath(BuyukResimYol));
            return BuyukResimYol;
        }

        private string EtkinlikResimEkle(HttpPostedFileBase etkinlikResimYol)
        {
            Image image = Image.FromStream(etkinlikResimYol.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 365, Height = 200 });


            string uzanti = System.IO.Path.GetExtension(etkinlikResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "/Content/EtkinlikImgMedium/" + isim + uzanti;

            bimage.Save(Server.MapPath(yol));

            return yol;
        }

        public ActionResult Esya()
        {
            return View();
        }
        public ActionResult Tanitim()
        {
            return View();
        }
        public ActionResult Blog(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var blogVeri = db.Bloglar.OrderBy(m => m.BlogPaylasmaTarihi).ToPagedList<Blog>(_sayfaNo, 3);

            ViewData["SonBloglarData"] = db.Bloglar.OrderByDescending(x => x.BlogPaylasmaTarihi).Take(1).ToList();

            return View(blogVeri);
        }
        public ActionResult BlogEkle()
        {
            int id = Convert.ToInt32(Session["Kullanic"]);

            var sorgu = db.Kullanicilar.FirstOrDefault(x=>x.KullaniciID==id);

            return View();
        }

        [HttpPost]
        public ActionResult BlogEkle(Blog gelen)
        {
            Blog ekle = new Blog();

            ekle.BlogBasligi = gelen.BlogBasligi;
            ekle.BlogIcerik = gelen.BlogIcerik;
            ekle.BlogPaylasmaTarihi = DateTime.Now;
            ekle.BlogResimYol = gelen.BlogResimYol;
            return View();
        }


        private string BlogResimEkle(HttpPostedFileBase blogResimYol)
        {
            Image image = Image.FromStream(blogResimYol.InputStream);
            Bitmap bimage = new Bitmap(image, new Size { Width = 365, Height = 200 });


            string uzanti = System.IO.Path.GetExtension(blogResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "/Content/EtkinlikImgMedium/" + isim + uzanti;

            bimage.Save(Server.MapPath(yol));

            return yol;
        }

        public ActionResult BizeUlasın()
        {
            return View();
        }
        public ActionResult EtkinlikDurum()
        {
            
            return View();
        }
        public ActionResult DersNotuDurum()
        {
            return View();
        }

        public ActionResult BlogDetay(int blogID)
        {
            var sorgu = db.Bloglar.FirstOrDefault(x => x.BlogID == blogID);
            ViewData["SonBloglarData"] = db.Bloglar.OrderByDescending(x => x.BlogPaylasmaTarihi).Take(1).ToList();
            return View(sorgu);
        }

        public ActionResult SonBloglar()
        {
            return View();
        }
    }
}