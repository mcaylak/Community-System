namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EtkinlikTablosuDuzenleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kullanici", "Etkinlik_EtkinlikID", c => c.Int());
            CreateIndex("dbo.Kullanici", "Etkinlik_EtkinlikID");
            AddForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik", "EtkinlikID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "Etkinlik_EtkinlikID", "dbo.Etkinlik");
            DropIndex("dbo.Kullanici", new[] { "Etkinlik_EtkinlikID" });
            DropColumn("dbo.Kullanici", "Etkinlik_EtkinlikID");
        }
    }
}
