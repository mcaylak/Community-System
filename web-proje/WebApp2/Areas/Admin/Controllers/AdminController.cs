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
        // GET: Admin/Admin
        VeriContext db = new VeriContext();//Veri Tabani Baglantisi
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
        public ActionResult tanitimIslemleri()
        {
            return View();
        }
        public ActionResult tanitimEkle()
        {
            return View();
        }
        public ActionResult etkinlikIslemleri()
        {
            return View();
        }
        public ActionResult etkinlikEkle()
        {
            return View();
        }
        public ActionResult dersNotuIslemleri()
        {
            return View();
        }
        public ActionResult dersNotuEkle()
        {
            return View();
        }
        public ActionResult blogIslemleri()
        {
            return View();
        }
        public ActionResult blogEkle()
        {
            return View();
        }
        public ActionResult sliderIslemleri()
        {
            return View();
        }
        public ActionResult sliderEkle()
        {
            return View();
        }
    }
}