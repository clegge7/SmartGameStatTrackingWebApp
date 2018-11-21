namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamID_in_BoxScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoxScores", "teamID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BoxScores", "teamID");
        }
    }
}
