namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClinics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clinics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DoctorId = c.Int(nullable: false),
                        Times = c.String(),
                        Address = c.String(),
                        Doctor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Doctor_Id)
                .Index(t => t.Doctor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clinics", "Doctor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Clinics", new[] { "Doctor_Id" });
            DropTable("dbo.Clinics");
        }
    }
}
