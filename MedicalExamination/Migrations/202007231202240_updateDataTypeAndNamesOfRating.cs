namespace MedicalExamination.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDataTypeAndNamesOfRating : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "RatingValue", c => c.Double(nullable: false));
        }
    }
}
