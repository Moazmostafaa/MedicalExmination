namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostDate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PostDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "CommentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentDate", c => c.DateTime());
            AlterColumn("dbo.Posts", "PostDate", c => c.DateTime());
        }
    }
}
