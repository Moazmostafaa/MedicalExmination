namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false),
                        GovernorateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Governorates", t => t.GovernorateId, cascadeDelete: true)
                .Index(t => t.GovernorateId);
            
            CreateTable(
                "dbo.Governorates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GovernorateName = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "GovernorateId", "dbo.Governorates");
            DropForeignKey("dbo.Governorates", "CountryId", "dbo.Countries");
            DropIndex("dbo.Governorates", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "GovernorateId" });
            DropTable("dbo.Countries");
            DropTable("dbo.Governorates");
            DropTable("dbo.Cities");
        }
    }
}
