namespace WebApp2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrunKategori : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Urunler", "Kategoriler_KategoriID", "dbo.UrunKategori");
            DropIndex("dbo.Urunler", new[] { "Kategoriler_KategoriID" });
            AddColumn("dbo.Urunler", "Kategoriler", c => c.Int(nullable: false));
            DropColumn("dbo.Urunler", "Kategoriler_KategoriID");
            DropTable("dbo.UrunKategori");
        }
        
        public override void Down()
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
            DropColumn("dbo.Urunler", "Kategoriler");
            CreateIndex("dbo.Urunler", "Kategoriler_KategoriID");
            AddForeignKey("dbo.Urunler", "Kategoriler_KategoriID", "dbo.UrunKategori", "KategoriID");
        }
    }
}
