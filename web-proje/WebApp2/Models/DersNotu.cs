using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class DersNotu
    {
        public int DersID { get; set; }
        public int DersAdi { get; set; }
        public string DersResimYol { get; set; }
        public string DersBaslıgı { get; set; }
        public virtual Kullanici Paylasan { get; set; }
    }
}