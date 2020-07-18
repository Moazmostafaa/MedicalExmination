namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringDiseasesAndSymptomsWithCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Diseases", "SymptomsCategories_Id", "dbo.SymptomsCategories");
            DropForeignKey("dbo.Symptoms", "SymptomsCategory_Id", "dbo.SymptomsCategories");
            DropIndex("dbo.Diseases", new[] { "SymptomsCategories_Id" });
            DropIndex("dbo.Symptoms", new[] { "SymptomsCategory_Id" });
            AddColumn("dbo.Categories", "CreationDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Diseases", "CategoryId");
            CreateIndex("dbo.Symptoms", "CategoryId");
            AddForeignKey("dbo.Diseases", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Symptoms", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Diseases", "SymptomsCategories_Id");
            DropColumn("dbo.Symptoms", "SymptomsCategory_Id");
            DropTable("dbo.SymptomsCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SymptomsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameAr = c.String(nullable: false),
                        NameEn = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Symptoms", "SymptomsCategory_Id", c => c.Int());
            AddColumn("dbo.Diseases", "SymptomsCategories_Id", c => c.Int());
            DropForeignKey("dbo.Symptoms", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Diseases", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Symptoms", new[] { "CategoryId" });
            DropIndex("dbo.Diseases", new[] { "CategoryId" });
            DropColumn("dbo.Categories", "CreationDate");
            CreateIndex("dbo.Symptoms", "SymptomsCategory_Id");
            CreateIndex("dbo.Diseases", "SymptomsCategories_Id");
            AddForeignKey("dbo.Symptoms", "SymptomsCategory_Id", "dbo.SymptomsCategories", "Id");
            AddForeignKey("dbo.Diseases", "SymptomsCategories_Id", "dbo.SymptomsCategories", "Id");
        }
    }
}
