using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class Blog
    {
        public int BlogID { get; set; }
        public int YazarID { get; set;}
        public string BlogResimYol { get; set; }
        public string BlogIcerik { get; set; }
        public string BlogBaslıgı { get; set; }
        public virtual Kullanici Yazar { get; set; }
    }
}