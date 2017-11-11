using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApp2.Models;


namespace WebApp2.veri
{
    public class VeriContext:DbContext
    {
        public VeriContext() : base("veriContext")
        {

        }

        DbSet<Blog> Bloglar { get; set; }
        DbSet<DersNotu> Dersler { get; set; }
        DbSet<Etkinlik> Etkinlikler { get; set; }
        DbSet<Kullanici> Kullanicilar { get; set; }
        DbSet<Menu> Menuler { get; set; }
        DbSet<Slider> Sliderlar { get; set; }
        DbSet<Tanitim> Tanitimlar { get; set; }
        DbSet<Urunler> Urun { get; set; }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}