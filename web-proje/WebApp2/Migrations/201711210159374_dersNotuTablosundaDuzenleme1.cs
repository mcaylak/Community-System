namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dersNotuTablosundaDuzenleme1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DersNotu", "paylasanAdi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DersNotu", "paylasanAdi");
        }
    }
}
