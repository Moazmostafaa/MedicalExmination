namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CityId");
            AddForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CityId", "dbo.Cities");
            DropIndex("dbo.AspNetUsers", new[] { "CityId" });
            DropColumn("dbo.AspNetUsers", "CityId");
        }
    }
}
