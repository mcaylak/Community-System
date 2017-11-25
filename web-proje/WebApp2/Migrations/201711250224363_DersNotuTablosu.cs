namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DersNotuTablosu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersNotu", "SayfaNumarasi", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DersNotu", "SayfaNumarasi");
        }
    }
}
