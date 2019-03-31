namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Game_Fix_20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "StartGame", c => c.DateTime());
            AlterColumn("dbo.Games", "StartQ2", c => c.DateTime());
            AlterColumn("dbo.Games", "StartQ3", c => c.DateTime());
            AlterColumn("dbo.Games", "StartQ4", c => c.DateTime());
            AlterColumn("dbo.Games", "GameEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "GameEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "StartQ4", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "StartQ3", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "StartQ2", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "StartGame", c => c.DateTime(nullable: false));
        }
    }
}
