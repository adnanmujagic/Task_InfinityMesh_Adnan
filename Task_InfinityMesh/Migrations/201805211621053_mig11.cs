namespace Task_InfinityMesh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserBlogs", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.UserBlogs", "UserId", "dbo.Users");
            DropIndex("dbo.UserBlogs", new[] { "UserId" });
            DropIndex("dbo.UserBlogs", new[] { "BlogId" });
            AddColumn("dbo.Blogs", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Blogs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Blogs", "UserId");
            AddForeignKey("dbo.Blogs", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropTable("dbo.UserBlogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserBlogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Blogs", "UserId", "dbo.Users");
            DropIndex("dbo.Blogs", new[] { "UserId" });
            DropColumn("dbo.Blogs", "UserId");
            DropColumn("dbo.Blogs", "PublishDate");
            CreateIndex("dbo.UserBlogs", "BlogId");
            CreateIndex("dbo.UserBlogs", "UserId");
            AddForeignKey("dbo.UserBlogs", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserBlogs", "BlogId", "dbo.Blogs", "Id", cascadeDelete: true);
        }
    }
}
