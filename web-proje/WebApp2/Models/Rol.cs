using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}