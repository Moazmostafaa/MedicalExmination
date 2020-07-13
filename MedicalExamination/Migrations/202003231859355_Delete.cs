namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Post_Caregory_Id", "dbo.Post_Category");
            DropForeignKey("dbo.Posts", "Patient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Post_Caregory_Id" });
            DropIndex("dbo.Posts", new[] { "Patient_Id" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.Posts");
            DropTable("dbo.Post_Category");
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Post_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostContent = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        CaregoryId = c.Int(nullable: false),
                        Post_Caregory_Id = c.Int(),
                        Patient_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.Posts", "Patient_Id");
            CreateIndex("dbo.Posts", "Post_Caregory_Id");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "Patient_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Post_Caregory_Id", "dbo.Post_Category", "Id");
        }
    }
}
