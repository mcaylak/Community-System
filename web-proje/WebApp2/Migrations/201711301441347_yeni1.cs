namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeni1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "BlogBasligi", c => c.String());
            DropColumn("dbo.Blog", "BlogBaslıgı");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blog", "BlogBaslıgı", c => c.String());
            DropColumn("dbo.Blog", "BlogBasligi");
        }
    }
}
