using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartGameStatTrackingWebApp.Models
{
    public class Game
    {
        public int id { get; set; }

        public int homeTeamID { get; set; }
        public IEnumerable<SelectListItem> homeTeamIDSLI { get; set; }

        public int awayTeamID { get; set; }

        [Display(Name ="Date")]
        public DateTime gameDate { get; set; }

        [Display(Name ="Home Team")]
        public string homeTeam { get; set; }

        [Display(Name ="Away Team")]
        public string awayTeam { get; set; }

        [Display(Name ="Home Points")]
        public int homePoints { get; set; }

        [Display(Name ="Away Points")]
        public int awayPoints { get; set; }
    }
}