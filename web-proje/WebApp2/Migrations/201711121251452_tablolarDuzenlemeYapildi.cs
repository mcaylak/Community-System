namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablolarDuzenlemeYapildi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(),
                    })
                .PrimaryKey(t => t.RolId);
            
            AddColumn("dbo.Kullanici", "RolID", c => c.Int(nullable: false));
            CreateIndex("dbo.Kullanici", "RolID");
            AddForeignKey("dbo.Kullanici", "RolID", "dbo.Rol", "RolId", cascadeDelete: true);
            DropColumn("dbo.Kullanici", "KullaniciTipi");
            DropTable("dbo.Menu");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        MenuSira = c.Int(nullable: false),
                        MenuAdi = c.String(),
                        MenuDurum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID);
            
            AddColumn("dbo.Kullanici", "KullaniciTipi", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Kullanici", "RolID", "dbo.Rol");
            DropIndex("dbo.Kullanici", new[] { "RolID" });
            DropColumn("dbo.Kullanici", "RolID");
            DropTable("dbo.Rol");
        }
    }
}
