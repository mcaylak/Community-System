namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeni : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EtkinlikKullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropForeignKey("dbo.EtkinlikKullanici", "Kullanici_KullaniciID", "dbo.Kullanici");
            DropIndex("dbo.EtkinlikKullanici", new[] { "Etkinlik_EtkinlikID" });
            DropIndex("dbo.EtkinlikKullanici", new[] { "Kullanici_KullaniciID" });
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID", c => c.Int());
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID1", c => c.Int());
            AddColumn("dbo.Etkinlik", "Kullanici_KullaniciID", c => c.Int());
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID");
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID1");
            CreateIndex("dbo.Etkinlik", "Kullanici_KullaniciID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik", "EtkinlikID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID1", "dbo.Etkinlik", "EtkinlikID");
            AddForeignKey("dbo.Etkinlik", "Kullanici_KullaniciID", "dbo.Kullanici", "KullaniciID");
            DropTable("dbo.EtkinlikKullanici");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EtkinlikKullanici",
                c => new
                    {
                        Etkinlik_EtkinlikID = c.Int(nullable: false),
                        Kullanici_KullaniciID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Etkinlik_EtkinlikID, t.Kullanici_KullaniciID });
            
            DropForeignKey("dbo.Etkinlik", "Kullanici_KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID1", "dbo.Etkinlik");
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropIndex("dbo.Etkinlik", new[] { "Kullanici_KullaniciID" });
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID1" });
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID" });
            DropColumn("dbo.Etkinlik", "Kullanici_KullaniciID");
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID1");
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID");
            CreateIndex("dbo.EtkinlikKullanici", "Kullanici_KullaniciID");
            CreateIndex("dbo.EtkinlikKullanici", "Etkinlik_EtkinlikID");
            AddForeignKey("dbo.EtkinlikKullanici", "Kullanici_KullaniciID", "dbo.Kullanici", "KullaniciID", cascadeDelete: true);
            AddForeignKey("dbo.EtkinlikKullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik", "EtkinlikID", cascadeDelete: true);
        }
    }
}
