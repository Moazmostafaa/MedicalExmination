namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPost_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post_Category", "CategoryName", c => c.String());
            DropColumn("dbo.Post_Category", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Post_Category", "Name", c => c.String());
            DropColumn("dbo.Post_Category", "CategoryName");
        }
    }
}
