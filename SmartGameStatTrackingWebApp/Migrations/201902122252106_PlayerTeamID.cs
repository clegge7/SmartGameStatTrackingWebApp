namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayerTeamID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Team_ID", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "Team_ID" });
            AddColumn("dbo.Players", "Team_ID1", c => c.Int());
            CreateIndex("dbo.Players", "Team_ID1");
            AddForeignKey("dbo.Players", "Team_ID1", "dbo.Teams", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Team_ID1", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "Team_ID1" });
            DropColumn("dbo.Players", "Team_ID1");
            CreateIndex("dbo.Players", "Team_ID");
            AddForeignKey("dbo.Players", "Team_ID", "dbo.Teams", "ID");
        }
    }
}
