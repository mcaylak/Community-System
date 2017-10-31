using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class Kullanici
    {
        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string KullaniciMail { get; set; }
        public int KullaniciSifre { get; set; }
        public DateTime KullaniciKayitTarihi{ get; set; }
        public bool KullaniciTipi { get; set; }
        public virtual ICollection<Urunler> Urun { get; set; }
        public virtual ICollection<DersNotu> Notlar { get; set; }
    }
}