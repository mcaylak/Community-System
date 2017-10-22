using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    public class AdminController : Controller//Admin Panelindeki İslemlerin Yapıldıgı Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()//Urun Ekleme Sayfası
        {
            return View();
        }
        public ActionResult UrunListele()//Urun Listeleme
        {
            return View();
        }
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