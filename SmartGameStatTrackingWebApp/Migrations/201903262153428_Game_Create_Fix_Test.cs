namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game_Create_Fix_Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
