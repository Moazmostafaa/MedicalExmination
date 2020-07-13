namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Total_Rate", c => c.Single());
            AddColumn("dbo.AspNetUsers", "UsersRated", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UsersRated");
            DropColumn("dbo.AspNetUsers", "Total_Rate");
        }
    }
}
