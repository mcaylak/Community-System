namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Urunler_duzenleme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrunKategori",
                c => new
                    {
                        KategoriID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(),
                    })
                .PrimaryKey(t => t.KategoriID);
            
            AddColumn("dbo.Urunler", "Kategoriler_KategoriID", c => c.Int());
            CreateIndex("dbo.Urunler", "Kategoriler_KategoriID");
            AddForeignKey("dbo.Urunler", "Kategoriler_KategoriID", "dbo.UrunKategori", "KategoriID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urunler", "Kategoriler_KategoriID", "dbo.UrunKategori");
            DropIndex("dbo.Urunler", new[] { "Kategoriler_KategoriID" });
            DropColumn("dbo.Urunler", "Kategoriler_KategoriID");
            DropTable("dbo.UrunKategori");
        }
    }
}
