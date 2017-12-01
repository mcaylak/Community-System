using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp2.Models
{
    public class UrunKategori
    {
        [Key]
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }
    }
}