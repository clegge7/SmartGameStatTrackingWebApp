namespace SmartGameStatTrackingWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audio_file_contents_test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.audio_file_contents2",
                c => new
                    {
                        a_f_id = c.Int(nullable: false, identity: true),
                        file_text = c.String(maxLength: 8000, unicode: false),
                        file_name = c.String(maxLength: 8000, unicode: false),
                        manual_control = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.a_f_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.audio_file_contents2");
        }
    }
}
