using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using SmartGameStatTrackingWebApp.Models;

namespace SmartGameStatTrackingWebApp.Models
{

    public class Team
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Player> Players { get; set; }
        
        public int wins { get; set; }

        public int losses { get; set; }

        [Range(2018, Int32.MaxValue)]
        public int season { get; set; }
    }
}