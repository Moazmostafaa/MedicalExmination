namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uupdateRatingTypeToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Total_Rate", c => c.Double());
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AspNetUsers", "Total_Rate", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
