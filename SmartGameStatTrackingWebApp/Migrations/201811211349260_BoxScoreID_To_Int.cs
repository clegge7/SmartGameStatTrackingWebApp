namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoxScoreID_To_Int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BoxScores", "playerid", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BoxScores", "playerid", c => c.String());
        }
    }
}
