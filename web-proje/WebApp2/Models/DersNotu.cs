using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class DersNotu
    {
        public int DersNotuID { get; set; }
        public string DersAdi { get; set; }
        public string DersResimYol { get; set; }
        public string DersBaslıgı { get; set; }
        public virtual Kullanici Paylasan { get; set; }
        public DateTime DersNotuPaylasmaTarihi { get; set; }
        public string paylasanAdi { get; set; }
        public string DersNotuAciklama { get; set; }
    }
}