namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game_Times : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "StartGame", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "StartQ2", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "StartQ3", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "StartQ4", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "GameEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Games", "GameEnd");
            DropColumn("dbo.Games", "StartQ4");
            DropColumn("dbo.Games", "StartQ3");
            DropColumn("dbo.Games", "StartQ2");
            DropColumn("dbo.Games", "StartGame");
        }
    }
}
