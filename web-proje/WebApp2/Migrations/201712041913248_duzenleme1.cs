namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kullanici", "KontrolKodu", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kullanici", "KontrolKodu", c => c.String());
        }
    }
}
