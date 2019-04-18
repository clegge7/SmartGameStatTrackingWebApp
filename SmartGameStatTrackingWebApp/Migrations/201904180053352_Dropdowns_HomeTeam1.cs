namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dropdowns_HomeTeam1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "homeTeam", c => c.String());
            AlterColumn("dbo.Games", "awayTeam", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "awayTeam", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "homeTeam", c => c.String(nullable: false));
        }
    }
}
