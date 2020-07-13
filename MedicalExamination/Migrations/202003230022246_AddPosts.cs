namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostContent = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        CaregoryId = c.Int(nullable: false),
                        PatientId = c.String(maxLength: 128),
                        Post_Caregory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientId)
                .ForeignKey("dbo.Post_Category", t => t.Post_Caregory_Id)
                .Index(t => t.PatientId)
                .Index(t => t.Post_Caregory_Id);
            
            CreateTable(
                "dbo.Post_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentContent = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        PostId = c.Int(nullable: false),
                        DoctorId = c.String(),
                        Patient_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Patient_Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.Patient_Id);
            
            DropColumn("dbo.AspNetUsers", "UserId_Paatient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserId_Paatient", c => c.String());
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Patient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Post_Caregory_Id", "dbo.Post_Category");
            DropForeignKey("dbo.Posts", "PatientId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Patient_Id" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "Post_Caregory_Id" });
            DropIndex("dbo.Posts", new[] { "PatientId" });
            DropTable("dbo.Comments");
            DropTable("dbo.Post_Category");
            DropTable("dbo.Posts");
        }
    }
}
