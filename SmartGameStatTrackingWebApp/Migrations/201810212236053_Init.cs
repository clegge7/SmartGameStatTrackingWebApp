namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        wins = c.Int(nullable: false),
                        losses = c.Int(nullable: false),
                        season = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        name = c.String(),
                        team = c.String(),
                        gamesPlayed = c.Int(nullable: false),
                        points = c.Int(nullable: false),
                        rebounds = c.Int(nullable: false),
                        assists = c.Int(nullable: false),
                        blocks = c.Int(nullable: false),
                        steals = c.Int(nullable: false),
                        turnovers = c.Int(nullable: false),
                        personalFouls = c.Int(nullable: false),
                        technicalFouls = c.Int(nullable: false),
                        season = c.Int(nullable: false),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.Team_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "Team_ID", "dbo.Teams");
            DropIndex("dbo.Players", new[] { "Team_ID" });
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
        }
    }
}
