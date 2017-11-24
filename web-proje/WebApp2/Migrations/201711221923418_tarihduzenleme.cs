namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tarihduzenleme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kullanici", "KullaniciKayitTarihi", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kullanici", "KullaniciKayitTarihi", c => c.DateTime(nullable: false));
        }
    }
}
