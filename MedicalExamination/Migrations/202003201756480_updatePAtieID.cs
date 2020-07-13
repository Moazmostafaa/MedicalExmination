namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePAtieID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserId_Doctor");
            DropColumn("dbo.AspNetUsers", "Id_Patient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Id_Patient", c => c.Int());
            AddColumn("dbo.AspNetUsers", "UserId_Doctor", c => c.String());
        }
    }
}
