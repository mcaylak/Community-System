using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class Etkinlik
    {
        public int EtkinlikID { get; set; }
        public string EtkinlikIcerik { get; set; }
        public string EtkinlikResimYol { get; set; }
        public string EtkinlikBuyukResimYol { get; set; }
        public string EtkinlikBasligi { get; set; }
        public DateTime EtkinlikTarihi { get; set; }
        public string EtkinlikSahibi { get; set; }
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}