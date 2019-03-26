namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game_Create_Fix_Test_Undo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "gameDate", c => c.DateTime(nullable: false));
        }
    }
}
