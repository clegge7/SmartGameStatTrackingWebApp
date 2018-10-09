using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    /*public class ApplicationContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
    }*/

    public class Player
    {
        public int id { get; set; }

        public int number { get; set; }

        public string name { get; set; }

        public string team { get; set; }

        public int gamesPlayed { get; set; }

        public int points { get; set; }

        public int rebounds { get; set; }

        public int assists { get; set; }

        public int blocks { get; set; }

        public int steals { get; set; }

        public int turnovers { get; set; }

        public int personalFouls { get; set; }

        public int technicalFouls { get; set; }

        [Range(2018, Int32.MaxValue)]
        public int season { get; set; }
    }
}