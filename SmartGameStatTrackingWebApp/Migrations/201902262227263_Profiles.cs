namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ASPNetID = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profiles");
        }
    }
}
