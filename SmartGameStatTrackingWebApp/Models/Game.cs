using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int awayTeamID { get; set; }

        [Display(Name ="Date")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName="date")]
        public DateTime gameDate { get; set; }

        [Display(Name ="Home Team")]
        [Required]
        public string homeTeam { get; set; }

        [Display(Name ="Away Team")]
        [Required]
        public string awayTeam { get; set; }

        [Display(Name ="Home Points")]
        public int homePoints { get; set; }

        [Display(Name ="Away Points")]
        public int awayPoints { get; set; }

        [Display(Name = "Q1 Start")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime? StartGame { get; set; }

        [Display(Name = "Q2 Start")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime? StartQ2 { get; set; }

        [Display(Name = "Q3 Start")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime? StartQ3 { get; set; }

        [Display(Name = "Q4 Start")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime? StartQ4 { get; set; }

        [Display(Name = "Game End")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime? GameEnd { get; set; }

        //for dropdownlist
        public IEnumerable<SelectListItem> getTeams()
        {
            using (var context = new SportsTrackDBContext())
            {
                List<SelectListItem> Teams = context.Teams.AsNoTracking()
                    .OrderBy(x => x.Name).Select(x => new SelectListItem
                    {
                        Value = x.Name,
                        Text = x.Name
                    }).ToList();

                var head = new SelectListItem()
                {
                    Value = null,
                    Text = "Select Team"
                };
                Teams.Insert(0, head);
                return new SelectList(Teams, "Value", "Text");
            }
        }
    }

    
}