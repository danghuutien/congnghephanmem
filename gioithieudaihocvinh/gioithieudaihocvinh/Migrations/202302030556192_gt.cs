namespace gioithieudaihocvinh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Backgrounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatId = c.Int(nullable: false),
                        Title = c.String(),
                        Thumbnail = c.String(),
                        Created_at = c.DateTime(),
                        Update_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categorys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.String(),
                        Typecategory_id = c.Int(),
                        Name = c.String(),
                        Slug = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.DateTime(),
                        Updated_by = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created_by = c.String(),
                        Thumbnail = c.String(),
                        Content = c.String(),
                        CatId = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Update_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FullName = c.String(),
                        Message = c.String(),
                        Gmail = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Update_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Create_at = c.DateTime(nullable: false),
                        Update_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatId = c.Int(),
                        Title = c.String(),
                        Slug = c.String(),
                        Thumbnail = c.String(),
                        Excerpt = c.String(),
                        Content = c.String(),
                        IsHighlight = c.Boolean(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Slug = c.String(),
                        Answer = c.String(),
                        Content = c.String(),
                        Status = c.Boolean(nullable: false),
                        PostId = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Created_by = c.DateTime(),
                        Updated_by = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Typecategorys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        Created_at = c.DateTime(),
                        Update_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Gmail = c.String(),
                        Phone = c.Double(nullable: false),
                        Password = c.String(),
                        Created_at = c.DateTime(),
                        Update_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Typecategorys");
            DropTable("dbo.Questions");
            DropTable("dbo.Posts");
            DropTable("dbo.News");
            DropTable("dbo.Contacts");
            DropTable("dbo.Comments");
            DropTable("dbo.Categorys");
            DropTable("dbo.Backgrounds");
        }
    }
}
