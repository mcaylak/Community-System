namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class etkinlikTablosuDüzenleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Etkinlik", "EtkinlikSahibi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Etkinlik", "EtkinlikSahibi");
        }
    }
}
