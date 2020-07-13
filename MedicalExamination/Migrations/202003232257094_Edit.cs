namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PatientId", c => c.String(maxLength: 128));
            AddColumn("dbo.Comments", "Patient_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "PatientId");
            CreateIndex("dbo.Comments", "Patient_Id");
            AddForeignKey("dbo.Comments", "Patient_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "PatientId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "PatientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Patient_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Patient_Id" });
            DropIndex("dbo.Posts", new[] { "PatientId" });
            DropColumn("dbo.Comments", "Patient_Id");
            DropColumn("dbo.Posts", "PatientId");
        }
    }
}
