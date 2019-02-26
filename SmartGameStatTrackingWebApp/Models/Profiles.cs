using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartGameStatTrackingWebApp.Models
{
    public class Profiles
    {
        [Key]
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<int> TeamsFollowing { get; set; }
    }
}