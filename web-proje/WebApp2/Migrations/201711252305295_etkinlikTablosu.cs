namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class etkinlikTablosu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID1", "dbo.Etkinlik");
            DropForeignKey("dbo.Etkinlik", "Kullanici_KullaniciID", "dbo.Kullanici");
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID" });
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID1" });
            DropIndex("dbo.Etkinlik", new[] { "Kullanici_KullaniciID" });
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
            
            AddColumn("dbo.Etkinlik", "etkinlikSahibiId", c => c.Int(nullable: false));
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID");
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID1");
            DropColumn("dbo.Etkinlik", "Kullanici_KullaniciID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Etkinlik", "Kullanici_KullaniciID", c => c.Int());
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID1", c => c.Int());
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID", c => c.Int());
            DropForeignKey("dbo.EtkinlikKullanici", "Kullanici_KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.EtkinlikKullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropIndex("dbo.EtkinlikKullanici", new[] { "Kullanici_KullaniciID" });
            DropIndex("dbo.EtkinlikKullanici", new[] { "Etkinlik_EtkinlikID" });
            DropColumn("dbo.Etkinlik", "etkinlikSahibiId");
            DropTable("dbo.EtkinlikKullanici");
            CreateIndex("dbo.Etkinlik", "Kullanici_KullaniciID");
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID1");
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID");
            AddForeignKey("dbo.Etkinlik", "Kullanici_KullaniciID", "dbo.Kullanici", "KullaniciID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID1", "dbo.Etkinlik", "EtkinlikID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik", "EtkinlikID");
        }
    }
}
