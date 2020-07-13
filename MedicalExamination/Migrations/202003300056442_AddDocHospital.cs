namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocHospital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorHospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Times = c.String(),
                        DoctorId = c.String(),
                        HospitalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "DoctorHospital_Id", c => c.Int());
            AddColumn("dbo.Hospitals", "DoctorHospital_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DoctorHospital_Id");
            CreateIndex("dbo.Hospitals", "DoctorHospital_Id");
            AddForeignKey("dbo.AspNetUsers", "DoctorHospital_Id", "dbo.DoctorHospitals", "Id");
            AddForeignKey("dbo.Hospitals", "DoctorHospital_Id", "dbo.DoctorHospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hospitals", "DoctorHospital_Id", "dbo.DoctorHospitals");
            DropForeignKey("dbo.AspNetUsers", "DoctorHospital_Id", "dbo.DoctorHospitals");
            DropIndex("dbo.Hospitals", new[] { "DoctorHospital_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "DoctorHospital_Id" });
            DropColumn("dbo.Hospitals", "DoctorHospital_Id");
            DropColumn("dbo.AspNetUsers", "DoctorHospital_Id");
            DropTable("dbo.DoctorHospitals");
        }
    }
}
