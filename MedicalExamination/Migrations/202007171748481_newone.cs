namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newone : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Diseases", name: "SymptomsCategory_Id", newName: "SymptomsCategories_Id");
            RenameIndex(table: "dbo.Diseases", name: "IX_SymptomsCategory_Id", newName: "IX_SymptomsCategories_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Diseases", name: "IX_SymptomsCategories_Id", newName: "IX_SymptomsCategory_Id");
            RenameColumn(table: "dbo.Diseases", name: "SymptomsCategories_Id", newName: "SymptomsCategory_Id");
        }
    }
}
