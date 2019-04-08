namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audio_file_contents_test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.audio_file_contents2", "manual_control", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.audio_file_contents2", "manual_control", c => c.Int(nullable: false));
        }
    }
}
