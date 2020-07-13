namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "Patient_Id", newName: "UserId");
            RenameIndex(table: "dbo.Comments", name: "IX_Patient_Id", newName: "IX_UserId");
            DropColumn("dbo.Comments", "DoctorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "DoctorId", c => c.String());
            RenameIndex(table: "dbo.Comments", name: "IX_UserId", newName: "IX_Patient_Id");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "Patient_Id");
        }
    }
}
