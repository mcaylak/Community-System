using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string EtkinlikAdres { get; set; }
        public string EtkinlikBasligi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EtkinlikTarihi { get; set; }
        public string EtkinlikSahibi { get; set; }
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}