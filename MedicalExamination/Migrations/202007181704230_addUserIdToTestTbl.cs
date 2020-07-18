namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserIdToTestTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "UserId");
        }
    }
}
