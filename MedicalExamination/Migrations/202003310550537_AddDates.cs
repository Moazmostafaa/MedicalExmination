namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clinics", "ClinicName", c => c.String());
            AddColumn("dbo.Clinics", "DayName", c => c.String());
            AddColumn("dbo.Clinics", "From", c => c.String());
            AddColumn("dbo.Clinics", "To", c => c.String());
            AddColumn("dbo.DoctorHospitals", "DayName", c => c.String());
            AddColumn("dbo.DoctorHospitals", "From", c => c.String());
            AddColumn("dbo.DoctorHospitals", "To", c => c.String());
            DropColumn("dbo.Clinics", "Name");
            DropColumn("dbo.Clinics", "Times");
            DropColumn("dbo.DoctorHospitals", "Times");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorHospitals", "Times", c => c.String());
            AddColumn("dbo.Clinics", "Times", c => c.String());
            AddColumn("dbo.Clinics", "Name", c => c.String());
            DropColumn("dbo.DoctorHospitals", "To");
            DropColumn("dbo.DoctorHospitals", "From");
            DropColumn("dbo.DoctorHospitals", "DayName");
            DropColumn("dbo.Clinics", "To");
            DropColumn("dbo.Clinics", "From");
            DropColumn("dbo.Clinics", "DayName");
            DropColumn("dbo.Clinics", "ClinicName");
        }
    }
}
