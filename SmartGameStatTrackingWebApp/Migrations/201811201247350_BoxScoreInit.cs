namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoxScoreInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoxScores",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        gameid = c.Int(nullable: false),
                        number = c.Int(nullable: false),
                        player = c.String(),
                        playerid = c.String(),
                        points = c.Int(nullable: false),
                        rebounds = c.Int(nullable: false),
                        assists = c.Int(nullable: false),
                        blocks = c.Int(nullable: false),
                        steals = c.Int(nullable: false),
                        turnovers = c.Int(nullable: false),
                        personalFouls = c.Int(nullable: false),
                        technicalFouls = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BoxScores");
        }
    }
}
