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

        public DbSet<Blog> Bloglar { get; set; }
        public DbSet<DersNotu> Dersler { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Slider> Sliderlar { get; set; }
        public DbSet<Tanitim> Tanitimlar { get; set; }
        public DbSet<Urunler> Urun { get; set; }
        public DbSet<Rol> Roller { get; set; }
        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}