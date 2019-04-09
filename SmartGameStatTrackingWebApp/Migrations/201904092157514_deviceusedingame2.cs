namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceusedingame2 : DbMigration
    {
        public override void Up()
        {
            /*DropPrimaryKey("dbo.device_used_in_game");
            AlterColumn("dbo.device_used_in_game", "g_id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.device_used_in_game", "g_id");*/
        }
        
        public override void Down()
        {
            /*DropPrimaryKey("dbo.device_used_in_game");
            AlterColumn("dbo.device_used_in_game", "g_id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.device_used_in_game", "g_id");*/
        }
    }
}
