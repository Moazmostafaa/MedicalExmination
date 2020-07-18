namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestAndDiseasesNamesAndValidations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "DiseaseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Diseases", "NameAr", c => c.String(nullable: false));
            AlterColumn("dbo.Diseases", "NameEn", c => c.String(nullable: false));
            AlterColumn("dbo.Symptoms", "NameAr", c => c.String(nullable: false));
            AlterColumn("dbo.Symptoms", "NameEn", c => c.String(nullable: false));
            AlterColumn("dbo.SymptomsCategories", "NameAr", c => c.String(nullable: false));
            AlterColumn("dbo.SymptomsCategories", "NameEn", c => c.String(nullable: false));
            AlterColumn("dbo.Tests", "DiseaseNameEn", c => c.String(nullable: false));
            AlterColumn("dbo.Tests", "DiseaseNameAr", c => c.String(nullable: false));
            DropColumn("dbo.Tests", "DiseaseName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "DiseaseName", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "DiseaseNameAr", c => c.String());
            AlterColumn("dbo.Tests", "DiseaseNameEn", c => c.String());
            AlterColumn("dbo.SymptomsCategories", "NameEn", c => c.String());
            AlterColumn("dbo.SymptomsCategories", "NameAr", c => c.String());
            AlterColumn("dbo.Symptoms", "NameEn", c => c.String());
            AlterColumn("dbo.Symptoms", "NameAr", c => c.String());
            AlterColumn("dbo.Diseases", "NameEn", c => c.String());
            AlterColumn("dbo.Diseases", "NameAr", c => c.String());
            DropColumn("dbo.Tests", "DiseaseId");
        }
    }
}
