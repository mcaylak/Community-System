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
            string yol = "/Content/UrunImg/" + isim + uzanti;
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
        public ActionResult UrunDuzenle(Urunler urun, HttpPostedFileBase urunResimYol,string urunAdi,string urunAciklama,string urunSahibi,int urunFiyat,DateTime tarih)
        {
            Urunler urunDuzenle = db.Urun.FirstOrDefault(x => x.UrunlerID == urun.UrunlerID);
            urunDuzenle.UrunAdi = urunAdi;
            urunDuzenle.UrunFiyat = urunFiyat;
            urunDuzenle.UrunAciklama = urunAciklama;
            urunDuzenle.UrunPaylasmaTarihi = tarih;
           
            if (urunResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(urunDuzenle.UrunResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(urunDuzenle.UrunResimYol));
                }
                urunDuzenle.UrunResimYol = UrunResimEkle(urunResimYol);
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
        public ActionResult TanitimDuzenle(int tanitimId)
        {
            Tanitim tanitim = db.Tanitimlar.FirstOrDefault(x => x.TanitimID == tanitimId);
            return View(tanitim);
        }
        [HttpPost]
        public ActionResult TanitimDuzenle(Tanitim tanitim, HttpPostedFileBase tanitimResimYol,string tanitimIcerik)
        {
            Tanitim tanitimDuzenle = db.Tanitimlar.FirstOrDefault(x => x.TanitimID == tanitim.TanitimID);
            tanitimDuzenle.TanitimIcerik = tanitimIcerik;
           

            if (tanitimResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(tanitimDuzenle.TanitimResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(tanitimDuzenle.TanitimResimYol));
                }
                tanitimDuzenle.TanitimResimYol = TanitimResimEkle(tanitimResimYol);
            }
            db.SaveChanges();
            return RedirectToAction("tanitimIslemleri");
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
        public ActionResult etkinlikEkle(Etkinlik etkinlik, HttpPostedFileBase etkinlikResimYol,string etkinlikAdres,string etkinlikSahibi,string etkinlikAdi,DateTime etkinlikPaylasmaTarihi,DateTime etkinlikBitisTarihi, string etkinlikAciklama)
        {
            etkinlik.EtkinlikResimYol = EtkinlikResimEkle(etkinlikResimYol);
            etkinlik.EtkinlikBuyukResimYol = EtkinlikBuyukResimEkle(etkinlikResimYol);
            etkinlik.EtkinlikTarihi = etkinlikPaylasmaTarihi;
            etkinlik.EtkinlikBitis = etkinlikBitisTarihi;
            etkinlik.EtkinlikIcerik = etkinlikAciklama;
            etkinlik.EtkinlikBasligi = etkinlikAdi;
            etkinlik.EtkinlikSahibi = etkinlikSahibi;
            etkinlik.EtkinlikAdres = etkinlikAdres;
            etkinlik.EtkinlikDurum = "1";

            db.Etkinlikler.Add(etkinlik);
            db.SaveChanges();

            return RedirectToAction("etkinlikIslemleri");
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
            Bitmap bimage = new Bitmap(image,new Size {Width=365,Height=200 });
            
            
            string uzanti = System.IO.Path.GetExtension(etkinlikResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "/Content/EtkinlikImgMedium/" + isim + uzanti;
            
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
        public ActionResult EtkinlikDuzenle(int etkinlikId)
        {
            Etkinlik etkinlik = db.Etkinlikler.FirstOrDefault(x => x.EtkinlikID == etkinlikId);
            return View(etkinlik);
        }
        [HttpPost]
        public ActionResult EtkinlikDuzenle(Etkinlik etkinlik, HttpPostedFileBase etkinlikResimYol,string etkinlikSahibi,string etkinlikAdi,DateTime etkinlikPaylasmaTarihi,string etkinlikAciklama,string etkinlikDurum)
        {
            Etkinlik etkinlikDuzenle = db.Etkinlikler.FirstOrDefault(x => x.EtkinlikID == etkinlik.EtkinlikID);
            etkinlikDuzenle.EtkinlikSahibi = etkinlikSahibi;
            etkinlikDuzenle.EtkinlikBasligi = etkinlikAdi;
            etkinlikDuzenle.EtkinlikTarihi = etkinlikPaylasmaTarihi;
            etkinlikDuzenle.EtkinlikIcerik = etkinlikAciklama;
            etkinlikDuzenle.EtkinlikDurum = etkinlikDurum;

            if (etkinlikResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(etkinlikDuzenle.EtkinlikResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(etkinlikDuzenle.EtkinlikResimYol));
                }
                etkinlikDuzenle.EtkinlikResimYol = EtkinlikResimEkle(etkinlikResimYol);
                etkinlikDuzenle.EtkinlikBuyukResimYol = EtkinlikBuyukResimEkle(etkinlikResimYol);
            }
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
            ders.DersBuyukResimYol = DersNotuBuyukResimEkle(dersNotuResimYol);
            ders.DersNotuPaylasmaTarihi = paylasmaTarihi;
            ders.DersAdi = dersAdi;
            ders.paylasanAdi = paylasanAdi;
            ders.DersBaslıgı = dersBasligi;
            ders.DersNotuAciklama = dersNotuAciklama;
            ders.DersNotuDurum = "1";
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
            Bitmap bimage = new Bitmap(image,new Size { Width=301,Height=251});
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
        public ActionResult DersNotuDuzenle(int dersId)
        {
            DersNotu ders = db.Dersler.FirstOrDefault(x => x.DersNotuID == dersId);
            return View(ders);
        }
        [HttpPost]
        public ActionResult DersNotuDuzenle(DersNotu ders, HttpPostedFileBase dersNotuResimYol,string paylasanAdi,string dersAdi,DateTime paylasmaTarihi,string dersNotuAciklama,string dersBasligi,string dersNotuDurum)
        {
            DersNotu dersDuzenle = db.Dersler.FirstOrDefault(x => x.DersNotuID == ders.DersNotuID);
            dersDuzenle.paylasanAdi = paylasanAdi;
            dersDuzenle.DersAdi = dersAdi;
            dersDuzenle.DersNotuPaylasmaTarihi = paylasmaTarihi;
            dersDuzenle.DersNotuAciklama = dersNotuAciklama;
            dersDuzenle.DersBaslıgı = dersBasligi;
            dersDuzenle.DersNotuDurum = dersNotuDurum;
           
            if (dersNotuResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(dersDuzenle.DersResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(dersDuzenle.DersResimYol));
                }
                dersDuzenle.DersResimYol = DersNotuResimEkle(dersNotuResimYol);
                dersDuzenle.DersBuyukResimYol = DersNotuBuyukResimEkle(dersNotuResimYol);
            }
            db.SaveChanges();
            return RedirectToAction("dersNotuIslemleri");
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
            Bitmap bimage = new Bitmap(image,new Size {Width=833,Height=380 });
            string uzanti = System.IO.Path.GetExtension(blogResimYol.FileName);
            string isim = Guid.NewGuid().ToString().Replace("-", "");
            string yol = "/Content/BlogImg/" + isim + uzanti;
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

        public ActionResult BlogDuzenle(int blogId)
        {
            Blog blog = db.Bloglar.FirstOrDefault(x => x.BlogID == blogId);
            return View(blog);
        }
        [HttpPost]
        public ActionResult BlogDuzenle(Blog blog, HttpPostedFileBase blogResimYol,string blogBaslik,DateTime blogTarih,string blogAciklama)
        {
            Blog blogDuzenle = db.Bloglar.FirstOrDefault(x => x.BlogID == blog.BlogID);
            blogDuzenle.BlogBaslıgı = blogBaslik;
            blogDuzenle.BlogPaylasmaTarihi = blogTarih;
            blogDuzenle.BlogIcerik = blogAciklama;

           

            if (blogResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(blogDuzenle.BlogResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(blogDuzenle.BlogResimYol));
                }
                blogDuzenle.BlogResimYol = DersNotuResimEkle(blogResimYol);
            }
            db.SaveChanges();
            return RedirectToAction("blogIslemleri");
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
            string yol = "/Content/SliderImg/" + isim + uzanti;
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
        public ActionResult SliderDuzenle(int sliderId)
        {
            Slider slider = db.Sliderlar.FirstOrDefault(x => x.SliderID == sliderId);
            return View(slider);
        }
        [HttpPost]
        public ActionResult SliderDuzenle(Slider slider, HttpPostedFileBase sliderResimYol, string aciklama)
        {
            Slider sliderDuzenle = db.Sliderlar.FirstOrDefault(x => x.SliderID == slider.SliderID);
            sliderDuzenle.SliderAdi = aciklama;
            

            if (sliderResimYol != null)
            {
                if (System.IO.File.Exists(Server.MapPath(sliderDuzenle.SliderResimYol)))
                {
                    System.IO.File.Delete(Server.MapPath(sliderDuzenle.SliderResimYol));
                }
                sliderDuzenle.SliderResimYol = UrunResimEkle(sliderResimYol);
            }
            db.SaveChanges();
            return RedirectToAction("sliderIslemleri");
        }
        #endregion
    }
}