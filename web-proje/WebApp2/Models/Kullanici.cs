using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string KullaniciSifre { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime KullaniciKayitTarihi{ get; set; }
        public virtual ICollection<Urunler> Urun { get; set; }
        public virtual ICollection<DersNotu> Notlar { get; set; }
        public virtual ICollection<Etkinlik> Etkinlikler { get; set; }
        public int RolID { get; set; }
        public virtual Rol Rol { get; set; }
    }
}