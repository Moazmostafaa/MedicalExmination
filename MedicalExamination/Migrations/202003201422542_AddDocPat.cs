namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocPat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Id_Doctor", c => c.Int());
            AddColumn("dbo.AspNetUsers", "UserId_Doctor", c => c.String());
            AddColumn("dbo.AspNetUsers", "CardId", c => c.String());
            AddColumn("dbo.AspNetUsers", "Id_Patient", c => c.Int());
            AddColumn("dbo.AspNetUsers", "UserId_Paatient", c => c.String());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "UserId_Paatient");
            DropColumn("dbo.AspNetUsers", "Id_Patient");
            DropColumn("dbo.AspNetUsers", "CardId");
            DropColumn("dbo.AspNetUsers", "UserId_Doctor");
            DropColumn("dbo.AspNetUsers", "Id_Doctor");
        }
    }
}
