namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tanitimTablosundaDuzenleme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tanitim", "TanitimResimID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tanitim", "TanitimResimID", c => c.Int(nullable: false));
        }
    }
}
