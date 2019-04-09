namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceID_to_Games_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "DeviceID", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "GameID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "GameID", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "DeviceID");
        }
    }
}
