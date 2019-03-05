using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class Following
    {
        [Key]
        public int id { get; set; }

        public string userName { get; set; }

        public int teamID { get; set; }
    }
}