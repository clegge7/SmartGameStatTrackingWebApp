using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class Player
    {
        public int id { get; set; }

        [Display(Name = "#")]
        public int number { get; set; }

        [Display(Name = "Player")]
        public string name { get; set; }

        [Display(Name = "Team")]
        public string team { get; set; }

        [Display(Name = "GP")]
        public int gamesPlayed { get; set; }

        [Display(Name = "PPG")]
        public int points { get; set; }

        [Display(Name = "RPG")]
        public int rebounds { get; set; }

        [Display(Name = "APG")]
        public int assists { get; set; }

        [Display(Name = "BPG")]
        public int blocks { get; set; }

        [Display(Name = "SPG")]
        public int steals { get; set; }

        [Display(Name = "TO/G")]
        public int turnovers { get; set; }

        [Display(Name = "PF/G")]
        public int personalFouls { get; set; }

        [Display(Name = "TF/G")]
        public int technicalFouls { get; set; }

        public int? Team_ID { get; set; }

        [Display(Name = "Season")]
        [Range(2018, Int32.MaxValue)]
        public int season { get; set; }
    }
}