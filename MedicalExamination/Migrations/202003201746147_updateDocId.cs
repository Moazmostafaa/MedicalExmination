namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Id_Doctor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Id_Doctor", c => c.Int());
        }
    }
}
