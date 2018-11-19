namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Game : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        homeTeamID = c.Int(nullable: false),
                        awayTeamID = c.Int(nullable: false),
                        gameDate = c.DateTime(nullable: false),
                        homeTeam = c.String(),
                        awayTeam = c.String(),
                        homePoints = c.Int(nullable: false),
                        awayPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
        }
    }
}
