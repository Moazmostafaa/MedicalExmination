namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestAndDiseasesTbls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(),
                        NameEn = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        DiseaseSymptoms_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiseaseSymptoms", t => t.DiseaseSymptoms_Id)
                .Index(t => t.DiseaseSymptoms_Id);
            
            CreateTable(
                "dbo.DiseaseSymptoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiseaseId = c.Int(nullable: false),
                        SymptomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Symptoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(),
                        NameEn = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SymptomsCategory_Id = c.Int(),
                        DiseaseSymptoms_Id = c.Int(),
                        TestSymptoms_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SymptomsCategories", t => t.SymptomsCategory_Id)
                .ForeignKey("dbo.DiseaseSymptoms", t => t.DiseaseSymptoms_Id)
                .ForeignKey("dbo.TestSymptoms", t => t.TestSymptoms_Id)
                .Index(t => t.SymptomsCategory_Id)
                .Index(t => t.DiseaseSymptoms_Id)
                .Index(t => t.TestSymptoms_Id);
            
            CreateTable(
                "dbo.SymptomsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(),
                        NameEn = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        DiseaseName = c.Int(nullable: false),
                        DiseaseNameEn = c.String(),
                        DiseaseNameAr = c.String(),
                        TestSymptoms_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestSymptoms", t => t.TestSymptoms_Id)
                .Index(t => t.TestSymptoms_Id);
            
            CreateTable(
                "dbo.TestSymptoms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        SymptomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "TestSymptoms_Id", "dbo.TestSymptoms");
            DropForeignKey("dbo.Symptoms", "TestSymptoms_Id", "dbo.TestSymptoms");
            DropForeignKey("dbo.Symptoms", "DiseaseSymptoms_Id", "dbo.DiseaseSymptoms");
            DropForeignKey("dbo.Symptoms", "SymptomsCategory_Id", "dbo.SymptomsCategories");
            DropForeignKey("dbo.Diseases", "DiseaseSymptoms_Id", "dbo.DiseaseSymptoms");
            DropIndex("dbo.Tests", new[] { "TestSymptoms_Id" });
            DropIndex("dbo.Symptoms", new[] { "TestSymptoms_Id" });
            DropIndex("dbo.Symptoms", new[] { "DiseaseSymptoms_Id" });
            DropIndex("dbo.Symptoms", new[] { "SymptomsCategory_Id" });
            DropIndex("dbo.Diseases", new[] { "DiseaseSymptoms_Id" });
            DropTable("dbo.TestSymptoms");
            DropTable("dbo.Tests");
            DropTable("dbo.SymptomsCategories");
            DropTable("dbo.Symptoms");
            DropTable("dbo.DiseaseSymptoms");
            DropTable("dbo.Diseases");
        }
    }
}
