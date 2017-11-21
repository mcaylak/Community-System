namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dersNotuTablosundaDuzenleme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersNotu", "DersNotuPaylasmaTarihi", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DersNotu", "DersNotuPaylasmaTarihi");
        }
    }
}
