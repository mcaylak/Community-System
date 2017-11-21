using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp2.Models;
using WebApp2.veri;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller//Admin Panelindeki İslemlerin Yapıldıgı Controller
    {
#region veritabaniBaglantisi
        // GET: Admin/Admin
        VeriContext db = new VeriContext();//Veri Tabani Baglantisi
#endregion

#region Urunİslemleri
        [HttpGet]
        public ActionResult UrunEkle()//Urun Ekleme Sayfası
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrunEkle(Urunler urun, HttpPostedFileBase exampleInputFile,string urunAdi,string urunAciklama,DateTime tarih,int urunFiyat)//Urun Ekleme Sayfası
        {
            urun.UrunResimYol = UrunResimEkle(exampleInputFile);
            urun.UrunPaylasmaTarihi = tarih;
            urun.UrunAciklama =urunAciklama;
            urun.UrunAdi =urunAdi;
            urun.UrunFiyat =urunFiyat;
           
            db.Urun.Add(urun);
            db.SaveChanges();

            return RedirectToAction("UrunIslemleri");
        }

        private string UrunResimEkle(HttpPostedFileBase exampleInputFile)
        {
            Image image = Image.FromStream(exampleInputFile.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(exampleInputFile.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/UrunImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        public ActionResult UrunSil(int urunId)
        {
            Urunler data = db.Urun.Where(x => x.UrunlerID == urunId).SingleOrDefault();
            db.Urun.Remove(data);
            db.SaveChanges();

            return RedirectToAction("UrunIslemleri");
        }

        public ActionResult UrunIslemleri()//Urun Listeleme
        {
            return View(db.Urun.ToList());
        }
        
        public ActionResult UrunDuzenle(int urunId)
        {
            Urunler urun = db.Urun.FirstOrDefault(x => x.UrunlerID == urunId);
            return View(urun);
        }
        [HttpPost]
        public ActionResult UrunDuzenle(Urunler urun, HttpPostedFileBase urunResim)
        {
            Urunler urunDuzenle = db.Urun.FirstOrDefault(x => x.UrunlerID == urun.UrunlerID);
            urunDuzenle.UrunAdi = urun.UrunAdi;
            urunDuzenle.UrunFiyat = urun.UrunFiyat;
            urunDuzenle.UrunAciklama = urun.UrunAciklama;
            urunDuzenle.UrunPaylasmaTarihi = urun.UrunPaylasmaTarihi;
           
            if (urunResim != null)
            {
                if (System.IO.File.Exists(Server.MapPath(urunDuzenle.UrunResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(urunDuzenle.UrunResimYol));
                }
                urunDuzenle.UrunResimYol = UrunResimEkle(urunResim);
            }
            db.SaveChanges();
            return RedirectToAction("UrunIslemleri");
        }
        #endregion

#region tanitimIslemleri
        public ActionResult tanitimIslemleri()
        {
            return View(db.Tanitimlar.ToList());
        }
        public ActionResult tanitimEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult tanitimEkle(Tanitim tanitim, HttpPostedFileBase tanitimResimYol,string tanitimAciklama)
        {
            tanitim.TanitimResimYol = TanitimResimEkle(tanitimResimYol);
            tanitim.TanitimIcerik = tanitimAciklama;
         
            db.Tanitimlar.Add(tanitim);
            db.SaveChanges();

            return RedirectToAction("tanitimIslemleri");
        }
        public ActionResult TanitimSil(int tanitimId)
        {
            Tanitim data = db.Tanitimlar.Where(x => x.TanitimID == tanitimId).SingleOrDefault();
            db.Tanitimlar.Remove(data);
            db.SaveChanges();

            return RedirectToAction("tanitimIslemleri");
        }

        private string TanitimResimEkle(HttpPostedFileBase tanitimResimYol)
        {
            Image image = Image.FromStream(tanitimResimYol.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(tanitimResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/TanitimImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        #endregion

#region etkinlikIslemleri
        public ActionResult etkinlikIslemleri()
        {
            return View(db.Etkinlikler.ToList());
        }
        [HttpGet]
        public ActionResult etkinlikEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult etkinlikEkle(Etkinlik etkinlik, HttpPostedFileBase etkinlikResimYol,string etkinlikSahibi,string etkinlikAdi,DateTime etkinlikPaylasmaTarihi, string etkinlikAciklama)
        {
            etkinlik.EtkinlikResimYol = EtkinlikResimEkle(etkinlikResimYol);
            etkinlik.EtkinlikTarihi = etkinlikPaylasmaTarihi;
            etkinlik.EtkinlikIcerik = etkinlikAciklama;
            etkinlik.EtkinlikBasligi = etkinlikAdi;
            etkinlik.EtkinlikSahibi = etkinlikSahibi;
            

            db.Etkinlikler.Add(etkinlik);
            db.SaveChanges();

            return RedirectToAction("etkinlikIslemleri");
        }

        private string EtkinlikResimEkle(HttpPostedFileBase etkinlikResimYol)
        {
            Image image = Image.FromStream(etkinlikResimYol.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(etkinlikResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/EtkinlikImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        public ActionResult EtkinlikSil(int EtkinlikId)
        {
            Etkinlik data = db.Etkinlikler.Where(x => x.EtkinlikID == EtkinlikId).SingleOrDefault();
            db.Etkinlikler.Remove(data);
            db.SaveChanges();

            return RedirectToAction("etkinlikIslemleri");
        }
        #endregion

#region DersNotuIslemleri
        public ActionResult dersNotuIslemleri()
        {
            return View(db.Dersler.ToList());
        }
        public ActionResult dersNotuEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dersNotuEkle(DersNotu ders, HttpPostedFileBase dersNotuResimYol,string paylasanAdi,string dersAdi,DateTime paylasmaTarihi,string dersNotuAciklama,string dersBasligi)
        {
            ders.DersResimYol = DersNotuResimEkle(dersNotuResimYol);
            ders.DersNotuPaylasmaTarihi = paylasmaTarihi;
            ders.DersAdi = dersAdi;
            ders.paylasanAdi = paylasanAdi;
            ders.DersBaslıgı = dersBasligi;
            

            db.Dersler.Add(ders);
            db.SaveChanges();

            return RedirectToAction("dersNotuIslemleri");
        }
        public ActionResult DersNotuSil(int dersId)
        {
            DersNotu data = db.Dersler.Where(x => x.DersNotuID == dersId).SingleOrDefault();
            db.Dersler.Remove(data);
            db.SaveChanges();

            return RedirectToAction("dersNotuIslemleri");
        }
        private string DersNotuResimEkle(HttpPostedFileBase DersNotuResimYol)
        {
            Image image = Image.FromStream(DersNotuResimYol.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(DersNotuResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/UrunImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        #endregion

        #region BlogIslemleri
        public ActionResult blogIslemleri()
        {
            return View(db.Bloglar.ToList());
        }
        public ActionResult blogEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult blogEkle(Blog blog, HttpPostedFileBase blogResimYol,string blogBaslik,DateTime blogTarih,string blogAciklama)
        {
            blog.BlogResimYol = BlogResimEkle(blogResimYol);
            blog.BlogPaylasmaTarihi = blogTarih;
            blog.BlogBaslıgı = blogBaslik;
            blog.BlogIcerik = blogAciklama;

            db.Bloglar.Add(blog);
            db.SaveChanges();

            return RedirectToAction("blogIslemleri");
        }

        private string BlogResimEkle(HttpPostedFileBase blogResimYol)
        {
            Image image = Image.FromStream(blogResimYol.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(blogResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/BlogImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        public ActionResult blogSil(int blogId)
        {
            Blog data = db.Bloglar.Where(x => x.BlogID == blogId).SingleOrDefault();
            db.Bloglar.Remove(data);
            db.SaveChanges();

            return RedirectToAction("BlogIslemleri");
        }
        #endregion

        #region SliderIslemleri
        public ActionResult sliderIslemleri()
        {
            return View(db.Sliderlar.ToList());
        }
        public ActionResult sliderEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult sliderEkle(Slider slider, HttpPostedFileBase sliderResimYol,string aciklama)
        {
            slider.SliderResimYol = SliderResimEkle(sliderResimYol);
            slider.SliderAdi =aciklama;

            db.Sliderlar.Add(slider);
            db.SaveChanges();

            return RedirectToAction("sliderIslemleri");
        }

        private string SliderResimEkle(HttpPostedFileBase sliderResimYol)
        {
            Image image = Image.FromStream(sliderResimYol.InputStream);
            Bitmap bimage = new Bitmap(image);
            string uzanti = System.IO.Path.GetExtension(sliderResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "~/Content/SliderImg/" + isim + uzanti;
            bimage.Save(Server.MapPath(yol));
            return yol;
        }
        public ActionResult sliderSil(int sliderId)
        {
            Slider data = db.Sliderlar.Where(x => x.SliderID == sliderId).SingleOrDefault();
            db.Sliderlar.Remove(data);
            db.SaveChanges();

            return RedirectToAction("sliderIslemleri");
        }
        #endregion

    }
}