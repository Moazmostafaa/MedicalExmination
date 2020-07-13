namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditClinic : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clinics", new[] { "Doctor_Id" });
            DropColumn("dbo.Clinics", "DoctorId");
            RenameColumn(table: "dbo.Clinics", name: "Doctor_Id", newName: "DoctorId");
            AlterColumn("dbo.Clinics", "DoctorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clinics", "DoctorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clinics", new[] { "DoctorId" });
            AlterColumn("dbo.Clinics", "DoctorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Clinics", name: "DoctorId", newName: "Doctor_Id");
            AddColumn("dbo.Clinics", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clinics", "Doctor_Id");
        }
    }
}
