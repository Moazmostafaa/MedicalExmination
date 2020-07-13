namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Total_Rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Single(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Total_Rate", c => c.Single());
        }
    }
}
