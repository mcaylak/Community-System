namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenlemeEtkinlikTablosu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Etkinlik", "EtkinlikBuyukResimYol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Etkinlik", "EtkinlikBuyukResimYol");
        }
    }
}
