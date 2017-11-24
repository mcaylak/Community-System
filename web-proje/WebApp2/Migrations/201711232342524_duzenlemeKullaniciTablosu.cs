namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenlemeKullaniciTablosu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID" });
            CreateTable(
                "dbo.EtkinlikKullanici",
                c => new
                    {
                        Etkinlik_EtkinlikID = c.Int(nullable: false),
                        Kullanici_KullaniciID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etkinlik_EtkinlikID, t.Kullanici_KullaniciID })
                .ForeignKey("dbo.Etkinlik", t => t.Etkinlik_EtkinlikID, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.Kullanici_KullaniciID, cascadeDelete: true)
                .Index(t => t.Etkinlik_EtkinlikID)
                .Index(t => t.Kullanici_KullaniciID);
            
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID", c => c.Int());
            DropForeignKey("dbo.EtkinlikKullanici", "Kullanici_KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.EtkinlikKullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropIndex("dbo.EtkinlikKullanici", new[] { "Kullanici_KullaniciID" });
            DropIndex("dbo.EtkinlikKullanici", new[] { "Etkinlik_EtkinlikID" });
            DropTable("dbo.EtkinlikKullanici");
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik", "EtkinlikID");
        }
    }
}
