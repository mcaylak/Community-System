namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogID = c.Int(nullable: false, identity: true),
                        BlogResimYol = c.String(),
                        BlogIcerik = c.String(),
                        BlogBaslıgı = c.String(),
                        BlogPaylasmaTarihi = c.DateTime(nullable: false),
                        Yazar_KullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.BlogID)
                .ForeignKey("dbo.Kullanici", t => t.Yazar_KullaniciID)
                .Index(t => t.Yazar_KullaniciID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        KullaniciID = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(),
                        KullaniciSoyadi = c.String(),
                        KullaniciMail = c.String(),
                        KullaniciSifre = c.String(),
                        KullaniciKayitTarihi = c.DateTime(nullable: false),
                        RolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KullaniciID)
                .ForeignKey("dbo.Rol", t => t.RolID, cascadeDelete: true)
                .Index(t => t.RolID);
            
            CreateTable(
                "dbo.DersNotu",
                c => new
                    {
                        DersNotuID = c.Int(nullable: false, identity: true),
                        DersAdi = c.String(),
                        DersResimYol = c.String(),
                        DersBaslıgı = c.String(),
                        DersNotuPaylasmaTarihi = c.DateTime(nullable: false),
                        paylasanAdi = c.String(),
                        DersNotuAciklama = c.String(),
                        Paylasan_KullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.DersNotuID)
                .ForeignKey("dbo.Kullanici", t => t.Paylasan_KullaniciID)
                .Index(t => t.Paylasan_KullaniciID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.Urunler",
                c => new
                    {
                        UrunlerID = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(),
                        UrunFiyat = c.Int(nullable: false),
                        UrunPaylasmaTarihi = c.DateTime(nullable: false),
                        UrunAciklama = c.String(),
                        UrunResimYol = c.String(),
                        Satici_KullaniciID = c.Int(),
                    })
                .PrimaryKey(t => t.UrunlerID)
                .ForeignKey("dbo.Kullanici", t => t.Satici_KullaniciID)
                .Index(t => t.Satici_KullaniciID);
            
            CreateTable(
                "dbo.Etkinlik",
                c => new
                    {
                        EtkinlikID = c.Int(nullable: false, identity: true),
                        EtkinlikIcerik = c.String(),
                        EtkinlikResimYol = c.String(),
                        EtkinlikBasligi = c.String(),
                        EtkinlikTarihi = c.DateTime(nullable: false),
                        EtkinlikSahibi = c.String(),
                    })
                .PrimaryKey(t => t.EtkinlikID);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderID = c.Int(nullable: false, identity: true),
                        SliderAdi = c.String(),
                        SliderResimYol = c.String(),
                    })
                .PrimaryKey(t => t.SliderID);
            
            CreateTable(
                "dbo.Tanitim",
                c => new
                    {
                        TanitimID = c.Int(nullable: false, identity: true),
                        TanitimResimYol = c.String(),
                        TanitimIcerik = c.String(),
                    })
                .PrimaryKey(t => t.TanitimID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blog", "Yazar_KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Urunler", "Satici_KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "RolID", "dbo.Rol");
            DropForeignKey("dbo.DersNotu", "Paylasan_KullaniciID", "dbo.Kullanici");
            DropIndex("dbo.Urunler", new[] { "Satici_KullaniciID" });
            DropIndex("dbo.DersNotu", new[] { "Paylasan_KullaniciID" });
            DropIndex("dbo.Kullanici", new[] { "RolID" });
            DropIndex("dbo.Blog", new[] { "Yazar_KullaniciID" });
            DropTable("dbo.Tanitim");
            DropTable("dbo.Slider");
            DropTable("dbo.Etkinlik");
            DropTable("dbo.Urunler");
            DropTable("dbo.Rol");
            DropTable("dbo.DersNotu");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Blog");
        }
    }
}
