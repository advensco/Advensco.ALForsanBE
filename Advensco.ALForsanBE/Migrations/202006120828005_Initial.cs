namespace Advensco.ALForsanBE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Title = c.String(),
                        Caption = c.String(),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.Gallery_Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        CoverImage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.CoverImage_Id)
                .Index(t => t.CoverImage_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Offers", "CoverImage_Id", "dbo.Images");
            DropForeignKey("dbo.Images", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.Products", new[] { "Image_Id" });
            DropIndex("dbo.Offers", new[] { "CoverImage_Id" });
            DropIndex("dbo.Images", new[] { "Gallery_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Offers");
            DropTable("dbo.Images");
            DropTable("dbo.Galleries");
        }
    }
}
