namespace PlaceUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Adress = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false),
                        WebSite = c.String(),
                        Email = c.String(nullable: false),
                        OpeningDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        CategoryId = c.Guid(),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Guid(nullable: false),
                        LikeDislike = c.Boolean(nullable: false),
                        Review = c.String(nullable: false),
                        PlaceId = c.Guid(),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagPlaces",
                c => new
                    {
                        Tag_TagId = c.Guid(nullable: false),
                        Place_PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Place_PlaceId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Place_PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPlaces", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.TagPlaces", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.Feedbacks", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TagPlaces", new[] { "Place_PlaceId" });
            DropIndex("dbo.TagPlaces", new[] { "Tag_TagId" });
            DropIndex("dbo.Feedbacks", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "CategoryId" });
            DropTable("dbo.TagPlaces");
            DropTable("dbo.Tags");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Places");
            DropTable("dbo.Categories");
        }
    }
}
