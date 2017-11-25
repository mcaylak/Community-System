namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DersNotuTablosundaDuzenleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersNotu", "DersBuyukResimYol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DersNotu", "DersBuyukResimYol");
        }
    }
}
