namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Games2_ElectricBoogaloo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "homeTeam", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "awayTeam", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "awayTeam", c => c.String());
            AlterColumn("dbo.Games", "homeTeam", c => c.String());
        }
    }
}
