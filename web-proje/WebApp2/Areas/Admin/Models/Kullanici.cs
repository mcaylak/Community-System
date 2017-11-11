using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Kullanici
    {
        public int UserID;
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public List<int> Satilanlar { get; set; }//List of item ids that user has posted on sale
        public bool KulTipi { get; set; }//Whether it is a admin or a user
        public string ProfilResmi { get; set; } //Profile picture of user
        public DateTime KayitTarihi { get; set; } //Date this user has registered
        public string IletisimNo { get; set; } //Phone number of user

   


    }
}