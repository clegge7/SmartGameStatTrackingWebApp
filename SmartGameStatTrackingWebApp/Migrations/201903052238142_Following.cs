namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Following : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        teamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Profiles", "TeamsFollowing", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "TeamsFollowing");
            DropTable("dbo.Followings");
        }
    }
}
