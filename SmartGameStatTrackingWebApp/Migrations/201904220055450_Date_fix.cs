namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Date_fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "StartGame", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "StartGame", c => c.DateTime());
        }
    }
}
