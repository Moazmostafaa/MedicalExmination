namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHospital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Speciality_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Speciality_Id");
            AddForeignKey("dbo.AspNetUsers", "Speciality_Id", "dbo.Specialities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Speciality_Id", "dbo.Specialities");
            DropIndex("dbo.AspNetUsers", new[] { "Speciality_Id" });
            DropColumn("dbo.AspNetUsers", "Speciality_Id");
            DropTable("dbo.Specialities");
            DropTable("dbo.Hospitals");
        }
    }
}
