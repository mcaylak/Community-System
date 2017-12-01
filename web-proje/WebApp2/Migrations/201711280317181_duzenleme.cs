namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class duzenleme : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Mail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(),
                        Email = c.String(),
                        Telefon = c.String(),
                        Icerik = c.String(),
                        Universite = c.String(),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
