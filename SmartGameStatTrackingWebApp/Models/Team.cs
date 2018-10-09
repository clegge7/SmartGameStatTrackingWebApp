using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
    }

    public class Team
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<Player> Players { get; set; }
        
        public int wins { get; set; }

        public int losses { get; set; }

        [Range(2018, Int32.MaxValue)]
        public int season { get; set; }
    }
}