using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class BoxScore
    {
        public int id { get; set; }

        public int gameid { get; set; }

        public int number { get; set; }

        public string player { get; set; }

        public string playerid { get; set; }

        public int points { get; set; }

        public int rebounds { get; set; }

        public int assists { get; set; }

        public int blocks { get; set; }

        public int steals { get; set; }

        public int turnovers { get; set; }

        public int personalFouls { get; set; }

        public int technicalFouls { get; set; }
    }
}