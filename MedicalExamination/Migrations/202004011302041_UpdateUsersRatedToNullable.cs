namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUsersRatedToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "UsersRated", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "UsersRated", c => c.Int());
        }
    }
}
