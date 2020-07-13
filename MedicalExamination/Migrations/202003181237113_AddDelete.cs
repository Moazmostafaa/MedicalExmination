namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDelete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DoctorId");
            DropColumn("dbo.AspNetUsers", "CardId");
            DropColumn("dbo.AspNetUsers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "CardId", c => c.String());
            AddColumn("dbo.AspNetUsers", "DoctorId", c => c.String());
        }
    }
}
