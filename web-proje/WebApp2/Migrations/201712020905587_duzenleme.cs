namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kullanici", "RolID", "dbo.Rol");
            DropIndex("dbo.Kullanici", new[] { "RolID" });
            AlterColumn("dbo.Kullanici", "KullaniciKayitTarihi", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Kullanici", "RolID", c => c.Int(nullable: false));
            CreateIndex("dbo.Kullanici", "RolID");
            AddForeignKey("dbo.Kullanici", "RolID", "dbo.Rol", "RolId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kullanici", "RolID", "dbo.Rol");
            DropIndex("dbo.Kullanici", new[] { "RolID" });
            AlterColumn("dbo.Kullanici", "RolID", c => c.Int());
            AlterColumn("dbo.Kullanici", "KullaniciKayitTarihi", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.Kullanici", "RolID");
            AddForeignKey("dbo.Kullanici", "RolID", "dbo.Rol", "RolId");
        }
    }
}
