namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPost : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "PatientId", newName: "Patient_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_PatientId", newName: "IX_Patient_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_Patient_Id", newName: "IX_PatientId");
            RenameColumn(table: "dbo.Posts", name: "Patient_Id", newName: "PatientId");
        }
    }
}
