namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingDoctor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorId = c.String(maxLength: 128),
                        PatientId = c.String(maxLength: 128),
                        RatingValue = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DoctorId)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "PatientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "DoctorId", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "PatientId" });
            DropIndex("dbo.Ratings", new[] { "DoctorId" });
            DropTable("dbo.Ratings");
        }
    }
}
