namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profiles_Schema_Fix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profiles");
            AddColumn("dbo.Profiles", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Profiles", "UserName");
            DropColumn("dbo.Profiles", "id");
            DropColumn("dbo.Profiles", "ASPNetID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "ASPNetID", c => c.String());
            AddColumn("dbo.Profiles", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Profiles");
            DropColumn("dbo.Profiles", "UserName");
            AddPrimaryKey("dbo.Profiles", "id");
        }
    }
}
