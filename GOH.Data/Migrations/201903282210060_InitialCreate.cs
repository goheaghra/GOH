namespace GOH.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommenterName = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        BlogId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .Index(t => t.BlogId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Value = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .Index(t => t.Value, unique: true);
            
            CreateTable(
                "dbo.TagBlogs",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Blog_BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Blog_BlogId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Blogs", t => t.Blog_BlogId, cascadeDelete: true)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Blog_BlogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagBlogs", "Blog_BlogId", "dbo.Blogs");
            DropForeignKey("dbo.TagBlogs", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.TagBlogs", new[] { "Blog_BlogId" });
            DropIndex("dbo.TagBlogs", new[] { "Tag_TagId" });
            DropIndex("dbo.Tags", new[] { "Value" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropTable("dbo.TagBlogs");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.Blogs");
        }
    }
}
