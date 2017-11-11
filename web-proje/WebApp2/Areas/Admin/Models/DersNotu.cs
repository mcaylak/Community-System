using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApp2.Areas.Admin.Models
{
    public class DersNotu
    {
        public int DersID { get; set; }
        public int DersAdi { get; set; }
        public string DersResimYol { get; set; }
        public int PaylasanID { get; set; }
        public virtual Kullanici Satici { get; set; }
    }
}