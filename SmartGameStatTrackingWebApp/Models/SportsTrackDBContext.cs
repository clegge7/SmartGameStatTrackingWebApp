namespace SmartGameStatTrackingWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SportsTrackDBContext : DbContext
    {
        public SportsTrackDBContext()
            : base("name=SportsTrackDBContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.Player> Players { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.BoxScore> BoxScores { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.Profiles> Profiles { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.Following> Following { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.audio_file_contents> audio_File_Contents { get; set; }

        public System.Data.Entity.DbSet<SmartGameStatTrackingWebApp.Models.device_used_in_game> device_used_in_game { get; set; }
    }
}
