namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameID_to_Games_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "GameID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "GameID");
        }
    }
}
