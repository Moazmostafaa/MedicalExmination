namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegrateDiseaseWithCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diseases", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Diseases", "SymptomsCategory_Id", c => c.Int());
            CreateIndex("dbo.Diseases", "SymptomsCategory_Id");
            AddForeignKey("dbo.Diseases", "SymptomsCategory_Id", "dbo.SymptomsCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Diseases", "SymptomsCategory_Id", "dbo.SymptomsCategories");
            DropIndex("dbo.Diseases", new[] { "SymptomsCategory_Id" });
            DropColumn("dbo.Diseases", "SymptomsCategory_Id");
            DropColumn("dbo.Diseases", "CategoryId");
        }
    }
}
